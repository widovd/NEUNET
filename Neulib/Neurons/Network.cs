using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Numerics;
using Neulib.Serializers;
using static Neulib.Extensions.FloatExtensions;
using System.Collections;
using static System.Math;

namespace Neulib.Neurons
{
    public class Network : LayerList
    {
        // ----------------------------------------------------------------------------------------
        #region Network properties

        // Collection of output layers from other networks or sensor layers.
        // These layers are input for feedforward calculation.
        // The activation values change when learning this network.
        private readonly LayerList _inputs = new LayerList();

        /// <summary>
        /// Gets or sets a layer. The connections are updated.
        /// </summary>
        /// <param name="index">The index of the layer.</param>
        /// <returns>The layer referenced by index.</returns>
        public override Layer this[int index]
        {
            get { return base[index]; }
            set
            {
                base[index] = value;
                UpdateConnections(index);
            }
        }

        /// <summary>
        /// The output layer of this network.
        /// This output layer can be connected to the input collection of other networks using AddInput.
        /// </summary>
        public Layer Output { get => Count > 0 ? this[Count - 1] : null; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new instance of Network.
        /// </summary>
        public Network()
        {
        }

        /// <summary>
        /// Creates a new Network from the stream.
        /// </summary>
        public Network(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public override void Add(Layer layer)
        {
            base.Add(layer);
            UpdateConnections(Count - 1);
        }

        public override void Insert(int index, Layer layer)
        {
            base.Insert(index, layer);
            UpdateConnections(index);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Unit

        public override void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
            // The inputs must be defined first!
            if (_inputs.Count == 0)
                throw new InvalidValueException(nameof(_inputs.Count), _inputs.Count, 561761);
            base.Randomize(random, biasMagnitude, weightMagnitude);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region LayerList

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network public

        public void ClearInputs()
        {
            _inputs.Clear();
            UpdateConnections(0);
        }

        public void AddInput(Layer layer)
        {
            _inputs.Add(layer);
            UpdateConnections(0);
        }

        public void FeedForward(bool parallel)
        {
            ForEach(layer => layer.FeedForward(parallel));
        }

        public void FeedBackward(Single1D ys, CostFunctionEnum costFunction, bool parallel)
        {
            int count = Count;
            if (count < 1) return;
            this[count - 1].CalculateDeltas(ys, costFunction);
            for (int i = count - 2; i >= 0; i--)
            {
                this[i].FeedBackward(this[i + 1], parallel);
            }
        }

        public float GetCostAndDerivatives(
            SampleList samples, Single1D derivatives, MeasurementList measurements,
            CalculationArguments arguments)
        {
            const bool parallel = true; // For debugging set to false .
            CostFunctionEnum costFunction = arguments.settings.CostFunction;
            float lambda = arguments.settings.Lambda;
            int nSamples = samples.Count;
            int nCoeffs = derivatives.Count;
            float cost = 0f;
            Layer last = Output;
            for (int i = 0; i < nCoeffs; i++) derivatives[i] = 0f;
            for (int iSample = 0; iSample < nSamples; iSample++)
            {
                arguments.ThrowIfCancellationRequested();
                Sample sample = samples[iSample];
                Single1D measurement = measurements[iSample];
                _inputs.SetActivations(sample.Inputs, 0);
                FeedForward(parallel);
                last.GetActivations(measurement, 0);
                cost += CostFunction(measurement, sample.Requirements, costFunction);
                int weightCount = CountWeight();
                cost += 0.5f * lambda * SumWeightSqr() / weightCount; // regularization
                FeedBackward(sample.Requirements, costFunction, parallel);
                AddDerivatives(derivatives, 0, lambda / weightCount);
                arguments.reporter?.ReportProgress(iSample, nSamples);
            }
            arguments.reporter?.ReportProgress(0, nSamples);
            cost /= nSamples;
            for (int i = 0; i < nCoeffs; i++) derivatives[i] /= nSamples;
            return cost;
        }

        public void Learn(
            SampleList samples, CalculationArguments arguments)
        // samples = yjks
        {
            arguments.reporter?.WriteStart($"Learning the network using a subset of {samples.Count} random samples...");
            Stopwatch timer = new Stopwatch();
            timer.Start();

            int nSamples = samples.Count; // number of sample rows
            int nCoefficients = CoefficientCount();
            // Current biasses and weights of the neurons in this network:
            Single1D coefficients = new Single1D(nCoefficients);
            // The derivatives of the cost with respect to the biasses and weights:
            Single1D derivatives = new Single1D(nCoefficients);
            Single1D velocities = new Single1D(nCoefficients);
            velocities.Clear();
            MeasurementList measurements = new MeasurementList(nSamples, Output.Count);
            GetCoefficients(coefficients, 0);
            Minimization minimization = new Minimization()
            {
                MaxIter = arguments.settings.MaxIter,
                Eps = arguments.settings.Epsilon,
                Tol = arguments.settings.Tolerance,
            };
            float finalCost = minimization.MomentumBasedGradientDescent(coefficients, derivatives, velocities,
                (iter) =>
                {
                    SetCoefficients(coefficients, 0);
                    arguments.reporter?.ReportCoefficients(coefficients);
                    float cost = GetCostAndDerivatives(samples, derivatives, measurements, arguments);
                    arguments.reporter?.ReportCostAndDerivatives(cost, derivatives, measurements);
                    return cost;
                }, arguments.settings.LearningRate, arguments.settings.MomentumCoefficient);
            arguments.reporter?.WriteEnd($"The network has learned in {timer.Elapsed.TotalSeconds} s, and the final cost value is {finalCost:F4}.");
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network private

        private void UpdateConnections(int index)
        {
            if (Count == 0) return;
            Layer layer = this[index];
            layer.ClearConnections();
            if (index > 0)
                layer.AddConnections(base[index - 1]);
            else if (_inputs != null)
                layer.AddConnections(_inputs);
            if (index < Count - 1)
                this[index + 1].ClearAndAddConnections(layer);
        }


        private static float QuadraticCostFunction(Single1D aa, Single1D yy)
        {
            float cost = 0;
            int n = aa.Count;
            if (n != yy.Count) throw new UnequalValueException(n, yy.Count, 109047);
            for (int i = 0; i < n; i++)
            {
                cost += Sqr(aa[i] - yy[i]);
            }
            return cost / (2f * n);
        }

        private static float CrossEntropyCostFunction(Single1D aa, Single1D yy)
        {
            double cost = 0;
            int n = aa.Count;
            if (n != yy.Count) throw new UnequalValueException(n, yy.Count, 800520);
            int m = 0;
            for (int i = 0; i < n; i++)
            {
                double a = aa[i];
                if (double.IsNaN(a)) throw new InvalidValueException(nameof(a), a, 815821);
                if (a == 0d || a == 1d) continue; // These neurons are neglected because otherwise costfunction is infinite. 
                double y = yy[i];
                cost += y * Log(a) + (1d - y) * Log(1d - a);
                m++;
            }
            if (m == 0) throw new InvalidValueException(nameof(m), m, 440745);
            return -(float)cost / m;
        }

        private static float CostFunction(Single1D aa, Single1D yy, CostFunctionEnum costFunction)
        {
            return costFunction switch
            {
                CostFunctionEnum.Quadratic => QuadraticCostFunction(aa, yy),
                CostFunctionEnum.CrossEntropy => CrossEntropyCostFunction(aa, yy),
                _ => throw new InvalidCaseException(nameof(costFunction), costFunction, 386203),
            };
        }


        #endregion
    }
}

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
    public class Network : Unit, IList<Layer>
    {
        // ----------------------------------------------------------------------------------------
        #region Network properties

        private Layer _input;
        // The input layer. This layer is input for feedforward calculation.
        // The activation values change when learning this network.
        public Layer Input
        {
            get { return _input; }
            set
            {
                _input = value;
                UpdateConnections(0);
            }
        }

        /// <summary>
        /// The layers of this Network.
        /// </summary>
        private readonly List<Layer> _layers = new List<Layer>();

        /// <summary>
        /// The number of layers in this Network.
        /// </summary>
        public int Count { get => _layers != null ? _layers.Count : 0; }

        /// <summary>
        /// Gets or sets a layer.
        /// </summary>
        /// <param name="index">The index of the layer.</param>
        /// <returns>The layer referenced by index.</returns>
        public Layer this[int index]
        {
            get { return _layers[index]; }
            set
            {
                _layers[index] = value;
                UpdateConnections(index);
            }
        }

        public bool IsReadOnly => ((ICollection<Layer>)_layers).IsReadOnly;

        /// <summary>
        /// The output layer of this network.
        /// This output layer can be connected to the input collection of other networks using AddInput.
        /// </summary>
        public Layer Output { get => Count > 0 ? _layers[Count - 1] : null; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new Network.
        /// </summary>
        public Network()
        {
        }

        /// <summary>
        /// Creates a new Network.
        /// </summary>
        public Network(Layer input, params Layer[] layers)
        {
            Input = input ?? throw new VarNullException(nameof(input), 599110);
            int count = layers.Length;
            for (int i = 0; i < count; i++)
            {
                Layer layer = layers[i] ?? throw new VarNullException(nameof(layer), 357538);
                Add(layer); // The connection indices are updated
            }
        }

        /// <summary>
        /// Creates a new Network from the stream.
        /// </summary>
        public Network(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            Input = (Layer)stream.ReadValue(serializer) ?? throw new VarNullException(nameof(Input), 965402);
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                // Do not use this.Add()! The connection indices are read from stream.
                _layers.Add((Layer)stream.ReadValue(serializer));
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override object CreateNew()
        {
            Type type = GetType();
            if (type == null) throw new VarNullException("type", 119611);
            try
            {
                return Activator.CreateInstance(type, (Layer)Input.Clone());
            }
            catch (MissingMethodException ex)
            {
                throw new InvalidCodeException($"Activator.CreateInstance({type}) failed.", ex, 528966);
            }
        }

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Network value = o as Network ?? throw new InvalidTypeException(o, nameof(Network), 321155);
            Input = (Layer)value.Input.Clone(); // Already done in Clone(), but ok...
            Clear();
            // Do not use this.Add()! The connection indices are copied from value.
            value.ForEach(layer => _layers.Add((Layer)layer.Clone()));
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteValue(Input, serializer);
            stream.WriteInt(Count);
            ForEach(layer => stream.WriteValue(layer, serializer));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Unit

        public override int CoefficientCount()
        {
            int count = 0;
            ForEach(layer => count += layer.CoefficientCount());
            return count;
        }

        public override int SetCoefficients(Single1D coefficients, int index)
        {
            ForEach(layer => index = layer.SetCoefficients(coefficients, index));
            return index;
        }

        public override int GetCoefficients(Single1D coefficients, int index)
        {
            ForEach(layer => index = layer.GetCoefficients(coefficients, index));
            return index;
        }

        public int AddDerivatives(Single1D derivatives, int index, float lambdaDivN)
        {
            Layer layer = Input;
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                Layer prevLayer = layer;
                layer = _layers[i];
                index = layer.AddDerivatives(prevLayer, derivatives, index, lambdaDivN);
            }
            return index;
        }

        public override void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
            ForEach(layer => layer.Randomize(random, biasMagnitude, weightMagnitude));
        }

        public override int CountWeight()
        {
            int count = 0;
            ForEach(layer => count += layer.CountWeight());
            return count;
        }

        public override float SumWeightSqr()
        {
            float sum = 0f;
            ForEach(layer => sum += layer.SumWeightSqr());
            return sum;
        }

        public override int SetActivations(Single1D activations, int index)
        {
            ForEach(layer => index = layer.SetActivations(activations, index));
            return index;
        }

        public override int GetActivations(Single1D activations, int index)
        {
            ForEach(layer => index = layer.GetActivations(activations, index));
            return index;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network public

        public void FeedForward(bool parallel)
        {
            Layer layer = Input;
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                Layer prevLayer = layer;
                layer = _layers[i];
                layer.FeedForward(prevLayer, parallel);
            }
        }

        public void FeedBackward(Single1D ys, CostFunctionEnum costFunction, bool parallel)
        {
            int count = Count;
            if (count == 0) return;
            Layer layer = _layers[count - 1];
            layer.CalculateDeltas(ys, costFunction);
            for (int i = count - 2; i >= 0; i--)
            {
                Layer nextLayer = layer;
                layer = _layers[i];
                layer.FeedBackward(nextLayer, parallel);
            }
        }

        public float GetCostAndDerivatives(
            SampleList samples, Single1D derivatives, MeasurementList measurements,
            CalculationSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            const bool parallel = false; // For debugging set to false .
            CostFunctionEnum costFunction = settings.CostFunction;
            float lambda = settings.Lambda;
            int nSamples = samples.Count;
            int nCoeffs = derivatives.Count;
            float cost = 0f;
            Layer last = Output;
            for (int i = 0; i < nCoeffs; i++) derivatives[i] = 0f;
            for (int iSample = 0; iSample < nSamples; iSample++)
            {
                tokenSource?.Token.ThrowIfCancellationRequested();
                Sample sample = samples[iSample];
                Single1D measurement = measurements[iSample];
                Input.SetActivations(sample.Inputs, 0);
                FeedForward(parallel);
                last.GetActivations(measurement, 0);
                cost += CostFunction(measurement, sample.Requirements, costFunction);
                int weightCount = CountWeight();
                cost += 0.5f * lambda * SumWeightSqr() / weightCount; // regularization
                FeedBackward(sample.Requirements, costFunction, parallel);
                AddDerivatives(derivatives, 0, lambda / weightCount);
                reporter?.ReportProgress(iSample, nSamples);
            }
            reporter?.ReportProgress(0, nSamples);
            cost /= nSamples;
            for (int i = 0; i < nCoeffs; i++) derivatives[i] /= nSamples;
            return cost;
        }

        public void Learn(
            SampleList samples, CalculationSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        // samples = yjks
        {
            reporter?.WriteStart($"Learning the network using a subset of {samples.Count} random samples...");
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
                MaxIter = settings.MaxIter,
                Eps = settings.Epsilon,
                Tol = settings.Tolerance,
            };
            float finalCost = minimization.MomentumBasedGradientDescent(coefficients, derivatives, velocities,
                (iter) =>
                {
                    SetCoefficients(coefficients, 0);
                    reporter?.ReportCoefficients(coefficients);
                    float cost = GetCostAndDerivatives(samples, derivatives, measurements, settings, reporter, tokenSource);
                    reporter?.ReportCostAndDerivatives(cost, derivatives, measurements);
                    return cost;
                }, settings.LearningRate, settings.MomentumCoefficient);
            reporter?.WriteEnd($"The network has learned in {timer.Elapsed.TotalSeconds} s, and the final cost value is {finalCost:F4}.");
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
                layer.AddConnections(_layers[index - 1]);
            else if (Input != null)
                layer.AddConnections(Input);
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
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(Layer layer)
        {
            return _layers.IndexOf(layer);
        }

        public void Add(Layer layer)
        {
            if (layer == null) throw new VarNullException(nameof(layer), 424420);
            _layers.Add(layer);
            UpdateConnections(Count - 1);
        }

        public void Insert(int index, Layer layer)
        {
            if (layer == null) throw new VarNullException(nameof(layer), 133650);
            _layers.Insert(index, layer);
            UpdateConnections(index);
        }

        public Layer Remove(int index)
        {
            Layer layer = _layers[index];
            _layers.RemoveAt(index);
            return layer;
        }

        public void RemoveAt(int index)
        {
            _layers.RemoveAt(index);
        }

        public bool Remove(Layer layer)
        {
            return _layers.Remove(layer);
        }

        public void Clear()
        {
            _layers.Clear();
        }

        public bool Contains(Layer item)
        {
            return _layers.Contains(item);
        }

        public void CopyTo(Layer[] array, int arrayIndex)
        {
            _layers.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Layer> GetEnumerator()
        {
            return ((IEnumerable<Layer>)_layers).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_layers).GetEnumerator();
        }

        public void ForEach(Action<Layer> action, bool parallel = false)
        {
            int count = _layers.Count;
            if (parallel)
                Parallel.For(0, count, j => action(_layers[j]));
            else
                for (int j = 0; j < count; j++) action(_layers[j]);
        }

        #endregion
    }
}

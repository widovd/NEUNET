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

        /// <summary>
        /// This layer contains the neurons which activation values are input for feedforward calculation.
        /// Neurons in this layer are not changed by this network.
        /// </summary>
        public Layer Input { get; private set; }

        /// <summary>
        /// The layers of this network. They are changed by feedforward and learning.
        /// </summary>
        private List<Layer> Layers { get; set; } = new List<Layer>();

        /// <summary>
        /// The number of layers in this network.
        /// </summary>
        public int Count { get => Layers.Count; }

        /// <summary>
        /// Gets or sets a layer.
        /// </summary>
        /// <param name="index">The index of the layer.</param>
        /// <returns>The layer referenced by index.</returns>
        /// <remarks>
        /// When a layer is set, then the connections of the old layer are removed,
        /// and the connections of the new layer are initialized (to layer [index - 1])
        /// </remarks>
        public Layer this[int index]
        {
            get { return Layers[index]; }
            set
            {
                Layers[index].ClearConnections();
                value.ClearConnections();
                if (index > 0) value.AddConnections(Layers[index - 1]);
                Layers[index] = value;
            }
        }

        /// <summary>
        /// The output layer of this network. If this network is empty, the input layer.
        /// </summary>
        public Layer Output { get => Layers != null && Layers.Count > 0 ? Layers[Count - 1] : Input; }

        public bool IsReadOnly => ((ICollection<Layer>)Layers).IsReadOnly;


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new instance of Network.
        /// </summary>
        /// <param name="inputLayer">The input layer.</param>
        public Network(Layer inputLayer)
        {
            Input = inputLayer;
        }

        /// <summary>
        /// Creates a new Network from the stream.
        /// </summary>
        public Network(Stream stream, BinarySerializer serializer)
        {
            Input = (Layer)stream.ReadValue(serializer);
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Add((Layer)stream.ReadValue(serializer));
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(Layer layer)
        {
            return Layers.IndexOf(layer);
        }

        public void Add(Layer layer)
        {
            Layer previous = Output;
            layer.ClearConnections();
            if (previous != null) layer.AddConnections(previous);
            Layers.Add(layer);
        }

        public void Insert(int index, Layer layer)
        {
            layer.ClearConnections();
            Layer previous = index >= 1 ? Layers[index - 1] : Input;
            if (previous != null) 
                layer.AddConnections(previous);
            Layers.Insert(index, layer);
            if (index < Count - 1)
            {
                Layer nextLayer = Layers[index + 1];
                nextLayer.ClearConnections();
                nextLayer.AddConnections(layer);
            }
        }

        public Layer Remove(int index)
        {
            Layer layer = Layers[index];
            layer.ClearConnections();
            Layers.RemoveAt(index);
            return layer;
        }

        public void RemoveAt(int index)
        {
            Remove(index);
        }

        public bool Remove(Layer layer)
        {
            layer.ClearConnections();
            return Layers.Remove(layer);
        }

        public void Clear()
        {
            ForEach(layer => layer.ClearConnections());
            Layers.Clear();
        }

        public bool Contains(Layer item)
        {
            return Layers.Contains(item);
        }

        public void CopyTo(Layer[] array, int arrayIndex)
        {
            Layers.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Layer> GetEnumerator()
        {
            return ((IEnumerable<Layer>)Layers).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Layers).GetEnumerator();
        }

        public void ForEach(Action<Layer> action, bool parallel = false)
        {
            int count = Layers.Count;
            if (parallel)
                Parallel.For(0, count, j => action(Layers[j]));
            else
                for (int j = 0; j < count; j++) action(Layers[j]);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override object CreateNew()
        {
            Type type = GetType();
            if (type == null) throw new VarNullException("type", 564683);
            try
            {
                return Activator.CreateInstance(type, (Layer)Input.Clone());
            }
            catch (MissingMethodException ex)
            {
                throw new InvalidCodeException($"Activator.CreateInstance({type}) failed.", ex, 708686);
            }
        }

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Network value = o as Network ?? throw new InvalidTypeException(o, nameof(Network), 473835);
            Clear();
            value.ForEach(layer => Add((Layer)layer.Clone()));
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

        public override int AddDerivatives(Single1D derivatives, int index, float lambdaDivN)
        {
            ForEach(layer => index = layer.AddDerivatives(derivatives, index, lambdaDivN));
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

        public void FeedForward(bool parallel)
        {
            ForEach(layer => layer.FeedForward(parallel));
        }

        public void FeedBackward(Single1D ys, CostFunctionEnum costFunction, bool parallel)
        {
            int count = Count;
            if (count < 1) return;
            Layers[count - 1].CalculateDeltas(ys, costFunction);
            for (int i = count - 2; i >= 0; i--)
            {
                Layers[i].FeedBackward(Layers[i + 1], parallel);
            }
        }

        public float GetCostAndDerivatives(
            SampleList samples, Single1D derivatives, MeasurementList measurements,
            CalculationArguments arguments)
        {
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
                Input.SetActivations(sample.Inputs, 0);
                FeedForward(true);
                last.GetActivations(measurement, 0);
                cost += CostFunction(measurement, sample.Requirements, costFunction);
                int weightCount = CountWeight();
                cost += 0.5f * lambda * SumWeightSqr() / weightCount; // regularization
                FeedBackward(sample.Requirements, costFunction, true);
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
            if (n != yy.Count) throw new UnequalValueException(n, yy.Count, 109047);
            for (int i = 0; i < n; i++)
            {
                double a = aa[i];
                double y = yy[i];
                cost += y * Log(a) + (1 - y) * Log(1 - a);
            }
            return -(float)cost / n;
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

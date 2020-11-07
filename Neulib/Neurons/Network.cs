using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using Neulib.Exceptions;
using Neulib.Numerics;
using Neulib.Serializers;
using static Neulib.Extensions.FloatExtensions;
using System.Collections;
using static System.Math;

namespace Neulib.Neurons
{
    public class Network : BaseObject, IList<SingleLayer>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private List<SingleLayer> Layers { get; set; } = new List<SingleLayer>();

        public int Count => Layers.Count;

        public bool IsReadOnly => ((ICollection<SingleLayer>)Layers).IsReadOnly;

        public SingleLayer this[int index]
        {
            get { return Layers[index]; }
            set
            {
                Layers[index] = value;
                if (index > 0)
                {
                    SingleLayer prevLayer = Layers[index - 1];
                    prevLayer.Next = value;
                }
                if (index + 1 < Count)
                {
                    SingleLayer nextLayer = Layers[index + 1];
                    nextLayer.Previous = value;
                }
            }
        }

        public SingleLayer First { get => Layers.Count > 0 ? Layers[0] : null; }

        public SingleLayer Last { get => Layers.Count > 0 ? Layers[Layers.Count - 1] : null; }

        public int InputCount { get => First.Count; }

        public int OutputCount { get => Last.Count; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Network()
        {
        }

        public Network(Stream stream, BinarySerializer serializer)
        {
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Layers.Add((SingleLayer)stream.ReadValue(serializer));
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(SingleLayer layer)
        {
            return Layers.IndexOf(layer);
        }

        public void Insert(int index, SingleLayer layer)
        {
            Layers.Insert(index, layer);
            if (index > 0)
            {
                SingleLayer prevLayer = Layers[index - 1];
                prevLayer.Next = layer;
            }
            if (index + 1 < Count)
            {
                SingleLayer nextLayer = Layers[index + 1];
                nextLayer.Previous = layer;
            }
        }

        public void Add(SingleLayer layer)
        {
            Layers.Add(layer);
            if (Count > 1)
            {
                SingleLayer prevLayer = Layers[Count - 2];
                prevLayer.Next = layer;
            }
        }

        public void RemoveAt(int index)
        // HIER VERDER
        {
            SingleLayer oldLayer = Layers[index];
            Layers.RemoveAt(index);
            if (oldLayer.Previous != null) oldLayer.Previous.Next = oldLayer.Next;
            if (oldLayer.Next != null) oldLayer.Next.Previous = oldLayer.Previous;
        }

        public bool Remove(SingleLayer layer)
        {
            int index = Layers.IndexOf(layer);
            if (index < 0) return false;
            RemoveAt(index);
            return true;
        }

        public void Clear()
        {
            Layers.Clear();
        }

        public bool Contains(SingleLayer item)
        {
            return Layers.Contains(item);
        }

        public void CopyTo(SingleLayer[] array, int arrayIndex)
        {
            Layers.CopyTo(array, arrayIndex);
        }

        public IEnumerator<SingleLayer> GetEnumerator()
        {
            return Layers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Layers.GetEnumerator();
        }

        public void ForEach(Action<SingleLayer> action, bool skipFirst)
        {
            int count = Layers.Count;
            for (int i = skipFirst ? 1 : 0; i < count; i++)
            {
                action(Layers[i]);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Network value = o as Network ?? throw new InvalidTypeException(o, nameof(Network), 473835);
            Layers.Clear();
            int count = value.Layers.Count;
            for (int i = 0; i < count; i++)
                Layers.Add((SingleLayer)value.Layers[i].Clone());
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            int count = Layers.Count;
            stream.WriteInt(count);
            for (int i = 0; i < count; i++)
                stream.WriteValue(Layers[i], serializer);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network

        public void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
            ForEach(layer =>
            {
                layer.Randomize(random, biasMagnitude, weightMagnitude);
            }, true);
        }

        public void FeedForward(Single1D xs, Single1D ys)
        {
            if (xs == null) throw new VarNullException(nameof(xs), 850330);

            int count = Layers.Count;
            SingleLayer layer = null;
            for (int i = 0; i < count; i++)
            {
                SingleLayer prevLayer = layer;
                layer = Layers[i];
                if (prevLayer == null)
                    layer.SetActivationsFirstLayer(xs);
                else
                    layer.FeedForward(); // FeedForward(prevLayer)
            }
            layer.GetActivationsLastLayer(ys);
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
            for (int i = 0; i < nCoeffs; i++) derivatives[i] = 0f;
            for (int iSample = 0; iSample < nSamples; iSample++)
            {
                arguments.ThrowIfCancellationRequested();
                Sample sample = samples[iSample];
                Single1D measurement = measurements[iSample];
                FeedForward(sample.Inputs, measurement);
                cost += CostFunction(measurement, sample.Requirements, costFunction);
                cost += 0.5f * lambda * MeanWeightSqr(out int nWeight); // regularization
                FeedBackward(sample.Requirements, costFunction);
                AddDerivatives(derivatives, lambda / nWeight);
                arguments.reporter?.ReportProgress(iSample, nSamples);
            }
            arguments.reporter?.ReportProgress(0, nSamples);
            cost /= nSamples;
            for (int i = 0; i < nCoeffs; i++) derivatives[i] /= nSamples;
            return cost;
        }

        public void Learn(SampleList samples, CalculationArguments arguments)
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
            MeasurementList measurements = new MeasurementList(nSamples, OutputCount);
            GetCoefficients(coefficients);
            Minimization minimization = new Minimization()
            {
                MaxIter = arguments.settings.MaxIter,
                Eps = arguments.settings.Epsilon,
                Tol = arguments.settings.Tolerance,
            };
            float finalCost = minimization.MomentumBasedGradientDescent(coefficients, derivatives, velocities,
                (iter) =>
                {
                    SetCoefficients(coefficients);
                    arguments.reporter?.ReportCoefficients(coefficients);
                    float cost = GetCostAndDerivatives(samples, derivatives, measurements, arguments);
                    arguments.reporter?.ReportCostAndDerivatives(cost, derivatives, measurements);
                    return cost;
                }, arguments.settings.LearningRate, arguments.settings.MomentumCoefficient);
            arguments.reporter?.WriteEnd($"The network has learned in {timer.Elapsed.TotalSeconds} s, and the final cost value is {finalCost:F4}.");
        }

        /// <summary>
        /// Returns the total number of biasses and weights of all neurons in this network except the first layer.
        /// </summary>
        public int CoefficientCount()
        {
            int h = 0;
            ForEach(layer =>
            {
                layer.ForEach(neuron => h += neuron.Count + 1);
            }, true);
            return h;
        }

        public Single1D CreateCoefficients()
        {
            return new Single1D(CoefficientCount());
        }

        /// <summary>
        /// Sets the biasses and weights of all neurons in this network.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        public void SetCoefficients(Single1D coefficients)
        {
            int h = 0;
            ForEach(layer =>
            {
                layer.ForEach(neuron =>
                {
                    neuron.Bias = coefficients[h++];
                    neuron.ForEach(connection => connection.Weight = coefficients[h++]);
                });
            }, true);
        }

        /// <summary>
        /// Gets the biasses and weights of all neurons in this network.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        public void GetCoefficients(Single1D coefficients)
        {
            int h = 0;
            ForEach(layer =>
            {
                layer.ForEach((Neuron neuron) =>
                {
                    coefficients[h++] = neuron.Bias;
                    neuron.ForEach(connection => coefficients[h++] = connection.Weight);
                });
            }, true);
        }

        public Single1D GetCoefficients()
        {
            Single1D coefficients = CreateCoefficients();
            GetCoefficients(coefficients);
            return coefficients;
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

        private void FeedBackward(Single1D ys, CostFunctionEnum costFunction)
        {
            if (ys == null) throw new VarNullException(nameof(ys), 411263);
            int count = Layers.Count;
            SingleLayer layer = null;
            for (int i = count - 1; i >= 0; i--)
            {
                SingleLayer nextLayer = layer;
                layer = Layers[i];
                if (nextLayer == null)
                    layer.CalculateDeltasLastLayer(ys, costFunction);
                else
                    layer.FeedBackward(); // FeedBackward(nextLayer)
            }
        }

        /// <summary>
        /// Fills a float array with the biasses and weights of all neurons in this network.
        /// </summary>
        /// <param name="derivatives">The float array which must have been created.</param>
        private void AddDerivatives(Single1D derivatives, float lambdaDivN)
        {
            int h = 0;
            SingleLayer layer = null;
            int count = Layers.Count;
            for (int i = 0; i < count; i++)
            {
                SingleLayer prevLayer = layer;
                layer = Layers[i];
                if (prevLayer == null) continue;
                layer.ForEach(neuron =>
                {
                    float delta = neuron.Delta;
                    derivatives[h++] += delta; // dC/dbj
                    neuron.ForEach((connection, k) =>
                    {
                        // dC/dwjk + L2 regularization:
                        derivatives[h++] += prevLayer[k].Activation * delta + lambdaDivN * connection.Weight;
                    });
                });
            }
        }

        private float MeanWeightSqr(out int n)
        {
            float sum = 0f;
            n = 0;
            int count = Layers.Count;
            for (int i = 1; i < count; i++)
                sum += Layers[i].SumWeightSqr(ref n);
            return sum / n;
        }

        #endregion
    }
}

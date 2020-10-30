using System;
using System.IO;
using System.Collections.Generic;
using Neulib.Exceptions;
using Neulib.Numerics;
using Neulib.Serializers;
using static Neulib.Extensions.FloatExtensions;

namespace Neulib.Neurons
{
    public class Network : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public List<Layer> Layers { get; private set; }

        public Layer First { get => Layers.Count > 0 ? Layers[0] : null; }

        public Layer Last { get => Layers.Count > 0 ? Layers[Layers.Count - 1] : null; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Network()
        {
            Layers = new List<Layer>();
        }

        public Network(Stream stream, BinarySerializer serializer) : this()
        {
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Layers.Add((Layer)stream.ReadValue(serializer));
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
            {
                Layers.Add((Layer)value.Layers[i].Clone());
            }
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            int count = Layers.Count;
            stream.WriteInt(count);
            for (int i = 0; i < count; i++)
            {
                stream.WriteValue(Layers[i], serializer);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network

        public void AddLayer(int count, Random random, float biasMagnitude, float weightMagnitude)
        {
            Layer layer = new Layer();
            layer.InitializeNeurons(count, Last, random, biasMagnitude, weightMagnitude);
            Layers.Add(layer);
        }

        public void FeedForward(Single1D xs, Single1D ys)
        {
            if (xs == null) throw new VarNullException(nameof(xs), 850330);

            int count = Layers.Count;
            Layer layer = null;
            for (int i = 0; i < count; i++)
            {
                Layer prevLayer = layer;
                layer = Layers[i];
                if (prevLayer == null)
                    layer.SetActivations(xs);
                else
                    layer.Calculate(prevLayer);
            }
            layer.GetActivations(ys);
        }

        private static float CostFunction(Single1D ys1, Single1D ys2)
        {
            float cost = 0;
            int n = ys1.Count;
            if (n != ys2.Count) throw new UnequalValueException(n, ys2.Count, 109047);
            for (int i = 0; i < n; i++)
            {
                cost += Sqr(ys1[i] - ys2[i]);
            }
            return cost / (2f * n);
        }

        public float GetCostAndDerivatives(
            SampleList samples, Single1D derivatives, MeasurementList measurements)
        {
            int nSample = samples.Count;
            int nCoeff = derivatives.Count;
            float cost = 0f;
            for (int i = 0; i < nCoeff; i++) derivatives[i] = 0f;
            for (int iSample = 0; iSample < nSample; iSample++)
            {
                Sample sample = samples[iSample];
                Single1D measurement = measurements[iSample];
                FeedForward(sample.Inputs, measurement);
                cost += CostFunction(measurement, sample.Requirements);
                FeedBackward(sample.Requirements);
                AddDerivatives(derivatives);
            }
            cost /= nSample;
            for (int i = 0; i < nCoeff; i++) derivatives[i] /= nSample;
            return cost;
        }

        public void Learn(SampleList samples, CalculationArguments arguments)
        // samples = yjks
        {
            int nSample = samples.Count; // number of sample rows
            int nCoefficient = CoefficientCount();
            // Current biasses and weights of the neurons in this network:
            Single1D coefficients = new Single1D(nCoefficient);
            // The derivatives of the cost with respect to the biasses and weights:
            Single1D derivatives = new Single1D(nCoefficient);
            MeasurementList measurements = new MeasurementList(nSample, samples.NY);
            GetCoefficients(coefficients);
            Minimization minimization = new Minimization()
            {
                MaxIter = arguments.settings.MaxIter,
                Eps = arguments.settings.Epsilon,
                Tol = arguments.settings.Tolerance,
            };
            minimization.SteepestDescent(coefficients, derivatives, (iter) =>
            {
                arguments.ThrowIfCancellationRequested();
                SetCoefficients(coefficients);
                arguments.reporter?.ReportCoefficients(coefficients);
                float cost = GetCostAndDerivatives(samples, derivatives, measurements);
                arguments.reporter?.ReportCostAndDerivatives(cost, derivatives, measurements);
                return cost;
            }, arguments.settings.LearningRate);
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network

        public void ForEach(Action<Layer> action, bool skipFirst)
        {
            int count = Layers.Count;
            for (int i = skipFirst ? 1 : 0; i < count; i++)
            {
                action(Layers[i]);
            }
        }

        private void FeedBackward(Single1D ys)
        {
            if (ys == null) throw new VarNullException(nameof(ys), 411263);
            int count = Layers.Count;
            Layer layer = null;
            for (int i = count - 1; i >= 0; i--)
            {
                Layer nextLayer = layer;
                layer = Layers[i];
                if (nextLayer == null)
                    layer.CalculateDeltas(ys);
                else
                    layer.FeedBackward(nextLayer);
            }
        }


        /// <summary>
        /// Returns the total number of biasses and weights of all neurons in this network except the first layer.
        /// </summary>
        public int CoefficientCount()
        {
            int h = 0;
            ForEach(layer =>
            {
                layer.ForEach((Neuron neuron) =>
                {
                    h += neuron.Connections.Count + 1;
                });
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
                layer.ForEach((Neuron neuron) =>
                {
                    neuron.Bias = coefficients[h++];
                    neuron.ForEach(connection =>
                    {
                        connection.Weight = coefficients[h++];
                    });
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
                    neuron.ForEach(connection =>
                    {
                        coefficients[h++] = connection.Weight;
                    });
                });
            }, true);
        }

        public Single1D GetCoefficients()
        {
            Single1D coefficients = CreateCoefficients();
            GetCoefficients(coefficients);
            return coefficients;
        }

        /// <summary>
        /// Fills a float array with the biasses and weights of all neurons in this network.
        /// </summary>
        /// <param name="derivatives">The float array which must have been created.</param>
        private void AddDerivatives(Single1D derivatives)
        {
            int h = 0;
            Layer layer = null;
            int count = Layers.Count;
            for (int i = 0; i < count; i++)
            {
                Layer prevLayer = layer;
                layer = Layers[i];
                if (prevLayer == null) continue;
                layer.ForEach(neuron =>
                {
                    float delta = neuron.Delta;
                    derivatives[h++] += delta; // dC/dbj
                    neuron.ForEach((connection, k) =>
                    {
                        derivatives[h++] += prevLayer.Neurons[k].Activation * delta; // dC/dwjk
                    });
                });
            }
        }


        #endregion
    }
}

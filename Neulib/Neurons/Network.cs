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

        public void FeedForward(float[] xs, float[] ys)
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

        private static float CostFunction(float[] ys1, float[] ys2)
        {
            float cost = 0;
            int n = ys1.Length;
            if (n != ys2.Length) throw new UnequalValueException(n, ys2.Length, 109047);
            for (int i = 0; i < n; i++)
            {
                cost += Sqr(ys1[i] - ys2[i]);
            }
            return cost / (2f * n);
        }

        public void Learn(SampleList samples, CalculationArguments arguments)
        // samples = yjks
        {
            int nSample = samples.Count; // number of sample rows
            int o = CountCoefficients();
            float[] p = new float[o]; // Current biasses and weights of all neurons in this network
            float[] dC = new float[o]; // The derivatives of the merit function f with respect to the biasses and weights of all neurons in this network
            GetCoefficients(p);
            Minimization minimization = new Minimization()
            {
                MaxIter = arguments.settings.MaxIter,
                Eps = arguments.settings.Epsilon,
                Tol = arguments.settings.Tolerance,
            };
            minimization.SteepestDescent(p, dC, () =>
            {
                arguments.ThrowIfCancellationRequested();
                SetCoefficients(p);
                float c = 0f;
                for (int i = 0; i < o; i++) dC[i] = 0f;
                for (int k = 0; k < nSample; k++)
                {
                    Sample sample = samples[k];
                    FeedForward(sample.Xs, sample.Zs);
                    c += CostFunction(sample.Zs, sample.Ys);
                    FeedBackward(sample.Ys);
                    AddDerivatives(dC);
                }
                c /= nSample;
                for (int i = 0; i < o; i++) dC[i] /= nSample;
                arguments.reporter?.ReportIteration((float)Math.Sqrt(c));
                return c;
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

        private void FeedBackward(float[] ys)
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
        public int CountCoefficients()
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

        /// <summary>
        /// Updates the biasses and weights of all neurons in this network with the values of the float array.
        /// </summary>
        /// <param name="p">The float array.</param>
        public void SetCoefficients(float[] p)
        {
            int h = 0;
            ForEach(layer =>
            {
                layer.ForEach((Neuron neuron) =>
                {
                    neuron.Bias = p[h++];
                    neuron.ForEach(connection =>
                    {
                        connection.Weight = p[h++];
                    });
                });
            }, true);
        }

        /// <summary>
        /// Fills a float array with the biasses and weights of all neurons in this network.
        /// </summary>
        /// <param name="p">The float array which must have been created.</param>
        public void GetCoefficients(float[] p)
        {
            int h = 0;
            ForEach(layer =>
            {
                layer.ForEach((Neuron neuron) =>
                {
                    p[h++] = neuron.Bias;
                    neuron.ForEach(connection =>
                    {
                        p[h++] = connection.Weight;
                    });
                });
            }, true);
        }

        /// <summary>
        /// Fills a float array with the biasses and weights of all neurons in this network.
        /// </summary>
        /// <param name="dC">The float array which must have been created.</param>
        private void AddDerivatives(float[] dC)
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
                    dC[h++] += delta; // dC/dbj
                    neuron.ForEach((connection, k) =>
                    {
                        dC[h++] += prevLayer.Neurons[k].Activation * delta; // dC/dwjk
                    });
                });
            }
        }


        #endregion
    }
}

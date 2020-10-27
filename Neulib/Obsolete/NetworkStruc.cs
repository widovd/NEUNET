using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Numerics;
using static Neulib.Extensions.FloatExtensions;
using static System.Math;

namespace Neulib.NeuronsNew
{
    public class NetworkStruc
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Layer First { get; private set; }

        public Layer Last
        {
            get
            {
                Layer layer = First;
                while (layer?.Next != null)
                {
                    layer = layer.Next;
                }
                return layer;
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Network()
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network

        public void Append(Layer layer)
        {
            layer.Parent = this;
            if (First == null)
            {
                First = layer;
            }
            else
            {
                Layer last = Last;
                last.Next = layer;
                layer.Previous = last;
            }
            layer.InitializeNeurons();
        }

        public void FeedForward(float[] xs, float[] ys)
        {
            if (xs == null) throw new VarNullException(nameof(xs), 850330);
            Layer layer = First;
            if (layer == null) throw new VarNullException(nameof(layer), 332725);
            layer.SetActivations(xs);
            while (layer.Next != null)
            {
                layer = layer.Next;
                layer.Calculate();
            }
            layer.GetActivations(ys);
        }

        private static float CostFunction(float[] ys1, float[] ys2)
        {
            float cost = 0;
            int n = ys1.Length;
            for (int i = 0; i < n; i++)
            {
                cost += Sqr(ys1[i] - ys2[i]);
            }
            return cost;
        }


        public void Learn(SampleList samples, int maxIter)
        // samples = yjks
        {
            const float eta = 0.02f;
            int n = samples.Count; // number of sample rows
            float[] ys = new float[Last.Count];
            int o = NCoefs;
            float[] p = new float[o]; // Current biasses and weights of all neurons in this network
            float[] dC = new float[o]; // The derivatives of the merit function f with respect to the biasses and weights of all neurons in this network
            GetCoefficients(p);
            Minimization minimization = new Minimization() { MaxIter = maxIter };
            minimization.SteepestDescent(p, dC, () =>
            {
                SetCoefficients(p);
                float c = 0f;
                for (int i = 0; i < o; i++) dC[i] = 0f;
                for (int k = 0; k < n; k++)
                {
                    Sample sample = samples[k];
                    FeedForward(sample.Xs, ys);
                    c += CostFunction(ys, sample.Ys);
                    FeedBackward(sample.Ys);
                    AddDerivatives(dC);
                }
                c /= n;
                for (int i = 0; i < o; i++) dC[i] /= n;
                return c;
            }, eta);
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network

        protected void ForEach(Action<Layer> action, bool skipFirst)
        {
            Layer layer = First;
            if (skipFirst) layer = layer?.Next;
            while (layer != null)
            {
                action(layer);
                layer = layer.Next;
            }
        }

        private void FeedBackward(float[] ys)
        {
            if (ys == null) throw new VarNullException(nameof(ys), 411263);
            Layer layer = Last;
            if (layer == null) throw new VarNullException(nameof(layer), 230663);
            layer.CalculateDeltas(ys);
            while (layer.Previous != null)
            {
                layer = layer.Previous;
                layer.FeedBackward();
            }
        }


        /// <summary>
        /// Returns the total number of biasses and weights of all neurons in this network except the first layer.
        /// </summary>
        private int NCoefs
        {
            get
            {
                int sum = 0;
                ForEach(layer => { sum += layer.Count * (1 + layer.Previous.Count); }, true);
                return sum;
            }
        }

        /// <summary>
        /// Updates the biasses and weights of all neurons in this network with the values of the float array.
        /// </summary>
        /// <param name="p">The float array.</param>
        private void SetCoefficients(float[] p)
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
        private void GetCoefficients(float[] p)
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
            ForEach(layer =>
            {
                Layer prevLayer = layer.Previous;
                layer.ForEach(neuron =>
                {
                    float delta = neuron.Delta;
                    dC[h++] += delta; // dC/dbj
                    neuron.ForEach((connection, k) =>
                    {
                        dC[h++] += prevLayer[k].Activation * delta; // dC/dwjk
                    });
                });
            }, true);
        }




        #endregion
    }
}

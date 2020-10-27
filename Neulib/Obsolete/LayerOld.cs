using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Numerics;
using static Neulib.Extensions.FloatExtensions;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents a layer of neurons in a neural network.
    /// </summary>
    public class Layer
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The neural network which contains this layer.
        /// </summary>
        public Network Parent { get; set; }

        /// <summary>
        /// The previous layer, or null if this is the first layer.
        /// </summary>
        public Layer Previous { get; set; }

        /// <summary>
        /// The next layer, or null if this is the last layer.
        /// </summary>
        public Layer Next { get; set; }

        /// <summary>
        /// returns the number of neurons in this layer.
        /// </summary>
        public int Count { get => _neurons != null ? _neurons.Length : 0; }

        protected readonly Neuron[] _neurons;

        /// <summary>
        /// Returns the i-th neuron in this layer.
        /// </summary>
        /// <param name="i">The index of the neuron in this layer.</param>
        /// <returns>The neuron.</returns>
        public Neuron this[int i]
        {
            get => _neurons[i];
            set => _neurons[i] = value;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new layer of neurons.
        /// </summary>
        /// <param name="count">The number of neurons in this layer.</param>
        public Layer(int count)
        {
            _neurons = new Neuron[count];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Layer

        public void ForEach(Action<Neuron> action)
        {
            int count = Count;
            for (int j = 0; j < count; j++)
            {
                action(_neurons[j]);
            }
        }

        public void ForEach(Action<int, Neuron> action)
        {
            int count = Count;
            for (int j = 0; j < count; j++)
            {
                action(j, _neurons[j]);
            }
        }

        public void InitializeNeurons()
        {
            int count = Count;
            int prevCount = Previous != null ? Previous.Count : 0;
            for (int i = 0; i < count; i++)
            {
                _neurons[i] = new Neuron(prevCount);
            }
        }

        public void SetResults(float[] xs)
        {
            if (Count != xs.Length) throw new UnequalValueException(Count, xs.Length, 480461);
            ForEach((j, neuron) => { neuron.Activation = xs[j]; });
        }

        /// <summary> 
        /// Calculates the result values of the neurons in this layer using the result values of the neurons in the previous layer, 
        /// and the bias and weight values of the neurons in this layer.
        /// </summary>
        /// <param name="prevLayer">The previous layer, or null if this is the first layer.</param>
        /// <remarks>
        /// If this is the first layer then no values will change.
        /// </remarks>
        public void FeedForward(Func<float, float> activationFunction)
        {
            Layer prevLayer = Previous;
            if (prevLayer != null)
                ForEach(neuron => neuron.FeedForward(prevLayer, activationFunction));
        }

        /// <summary>
        /// Returns the total number of biasses and weights of all neurons in this layer.
        /// </summary>
        /// <returns>The total number of biasses and weights of all neurons in this layer.</returns>
        public int NCoefs
        {
            get
            {
                Layer previous = Previous;
                if (previous == null) throw new VarNullException(nameof(previous), 331080);
                return (previous.Count + 1) * Count;
            }
        }

        /// <summary>
        /// Fills a float array with the biasses and weights of all neurons in this layer. The float array must have been created.
        /// </summary>
        /// <param name="p">The float array.</param>
        public void GetCoefficients(float[] p)
        {
            int h = 0;
            ForEach((neuron) =>
            {
                p[h++] = neuron.Bias;
                neuron.ForEach(weight =>
                {
                    p[h++] = weight;
                });
            });
        }

        /// <summary>
        /// Updates the biasses and weights of all neurons in this layer with the values of the float array.
        /// </summary>
        /// <param name="p">The float array.</param>
        public void SetCoefficients(float[] p)
        {
            int h = 0;
            ForEach((neuron) =>
            {
                neuron.Bias = p[h++];
                neuron.ForEach(() =>
                {
                    return p[h++];
                });
            });
        }

        public void GetDerivatives(float[,] samples, float[] derivs)
        {
            int k = 0;
            ForEach((neuron) =>
            {
                derivs[k++] = neuron.Bias; // df/db
                neuron.ForEach(weight =>
                {
                    derivs[k++] = weight; // df/dw
                });
            });
        }



        //public void BackPropagate(SampleList samples)
        //// samples = yjks
        //{
        //    int m = Count; // the number of neurons in this layer
        //    int n = samples.Count; // number of sample rows
        //    //int m = samples.GetLength(1); // must be equal to the number of neurons in this layer
        //    //if (m != Count) throw new UnequalValueException(m, Count, 976303);
        //    int o = NCoefs;
        //    float[] p = new float[o]; // Current biasses and weights of all neurons in this layer
        //    float[] df = new float[o]; // The derivatives of the merit function f with respect to the biasses and weights of all neurons in this layer
        //    Layer previous = Previous;
        //    if (previous == null) throw new VarNullException(nameof(previous), 886747);


        //    GetCoefficients(p);

        //    Minimization minimization = new Minimization() { MaxIter = 100 };
        //    minimization.SteepestDescent(p, df, () =>
        //    {
        //        SetCoefficients(p);
        //        float f = 0f;
        //        for (int k = 0; k < n; k++) // the sample row index
        //        {
        //            Parent.FeedForward(samples[k].Xs);
        //            int h = 0; // index: df[h]
        //            ForEach((j, neuron) => // for each neuron j in this layer
        //            {
        //                f += Sqr(neuron.Activation - samples[k].Ys[j]); // dit werkt alleen voor de laatste laag !
        //                float dfdb = neuron.DerivativeOld(samples[k].Ys[j]); // dit werkt alleen voor de laatste laag !
        //                df[h++] += dfdb;
        //                previous.ForEach(prevNeuron => // for each neuron i in the previous layer
        //                {
        //                    df[h++] += dfdb * prevNeuron.Activation; // samples[k].Xs[i] and i niet correct
        //                });
        //            });
        //        }
        //        f /= 2 * n * m;
        //        for (int h = 0; h < o; h++)
        //        {
        //            df[h] /= n * m;
        //        }

        //        return f;
        //    }, 0.001f);
        //}


        public void Bla(float[] ys, Func<float, float> activationDerivative)
        {
            int m = Count; // the number of neurons in this layer
            ForEach((j, neuron) => // for each neuron j in this layer
            {
                neuron.Bla(ys[j], activationDerivative);
            });
        }

        public void Bloe()
        {

        }


        #endregion
    }
}

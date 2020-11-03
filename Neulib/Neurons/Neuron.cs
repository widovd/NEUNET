using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Serializers;
using static System.Math;
using System.Collections;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents a neuron for use in a neural network.
    /// </summary>
    public class Neuron : BaseObject, IList<Connection>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The connections with the neurons in the previous layer.
        /// </summary>
        private List<Connection> Connections { get; set; } = new List<Connection>();

        public int Count { get => Connections.Count; }

        public bool IsReadOnly => ((ICollection<Connection>)Connections).IsReadOnly;

        public Connection this[int index] { get => Connections[index]; set => Connections[index] = value; }

        /// <summary>
        /// The bias value of this neuron.
        /// </summary>
        public float Bias { get; set; } = 0f;

        /// <summary>
        /// The weighted input value of this neuron.
        /// </summary>
        public float Sum { get; private set; } = 0f;

        /// <summary>
        /// The activation value of this neuron.
        /// </summary>
        public float Activation { get; set; } = 0f;

        /// <summary>
        /// The delta error value of this neuron.
        /// </summary>
        public float Delta { get; set; } = 0f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new neuron.
        /// </summary>
        public Neuron()
        {
        }

        /// <summary>
        /// Creates a new neuron from the stream.
        /// </summary>
        public Neuron(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            Bias = stream.ReadSingle();
            Sum = stream.ReadSingle();
            Activation = stream.ReadSingle();
            Delta = stream.ReadSingle();
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Connections.Add((Connection)stream.ReadValue(serializer));
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(Connection item)
        {
            return Connections.IndexOf(item);
        }

        public void Insert(int index, Connection item)
        {
            Connections.Insert(index, item);
        }

        public void Add(Connection item)
        {
            Connections.Add(item);
        }

        public void RemoveAt(int index)
        {
            Connections.RemoveAt(index);
        }

        public bool Remove(Connection item)
        {
            return Connections.Remove(item);
        }

        public void Clear()
        {
            Connections.Clear();
        }

        public bool Contains(Connection item)
        {
            return Connections.Contains(item);
        }

        public void CopyTo(Connection[] array, int arrayIndex)
        {
            Connections.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Connection> GetEnumerator()
        {
            return Connections.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Connections.GetEnumerator();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return $"bias = {Bias:F3}, sum = {Sum:F3}, activation = {Activation:F3}, delta = {Delta:F3}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Neuron value = o as Neuron ?? throw new InvalidTypeException(o, nameof(Neuron), 166408);
            Bias = value.Bias;
            Sum = value.Sum;
            Activation = value.Activation;
            Delta = value.Delta;
            int count = value.Connections.Count;
            Connections.Clear();
            for (int i = 0; i < count; i++)
            {
                Connections.Add((Connection)value.Connections[i].Clone());
            }
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteSingle(Bias);
            stream.WriteSingle(Sum);
            stream.WriteSingle(Activation);
            stream.WriteSingle(Delta);
            int count = Connections.Count;
            stream.WriteInt(count);
            for (int i = 0; i < count; i++)
            {
                stream.WriteValue(Connections[i], serializer);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Neuron

        public void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
            Bias = (float)(2d * random.NextDouble() - 1d) * biasMagnitude;
            int count = Connections.Count;
            for (int i = 0; i < count; i++)
                Connections[i].Weight = (float)(2d * random.NextDouble() - 1d) * weightMagnitude;
        }

        public void ForEach(Action<Connection> action)
        {
            int count = Connections.Count;
            for (int i = 0; i < count; i++)
            {
                action(Connections[i]);
            }
        }

        public void ForEach(Action<Connection, int> action)
        {
            int count = Connections.Count;
            for (int i = 0; i < count; i++)
            {
                action(Connections[i], i);
            }
        }

        public static float ActivationFunction(float z)
        {
            return 1f / (1f + (float)Exp(-z));
        }

        public static float ActivationDerivative(float a)
        // a is the activation value
        {
            return a * (1f - a);
        }

        /// <summary> 
        /// Calculates the activation value of this neuron from the neurons in the previous layer, the bias, and the activation function.
        /// </summary>
        public void FeedForward(Layer prevLayer)
        {
            int count = Connections.Count;
            float sum = Bias;
            for (int k = 0; k < count; k++)
            {
                sum += Connections[k].Weight * prevLayer[k].Activation;
            }
            Sum = sum; // zl
            Activation = ActivationFunction(sum); // sl
        }

        public void FeedBackward(Layer nextLayer, int j)
        {
            float delta = 0f;
            int nextCount = nextLayer.Count;
            for (int k = 0; k < nextCount; k++)
            {
                Neuron nextNeuron = nextLayer[k];
                delta += nextNeuron[j].Weight * nextNeuron.Delta; // wji
            }
            delta *= Neuron.ActivationDerivative(Activation);
            Delta = delta;
        }


        /// <summary>
        /// Calculates Delta of this neuron which must be in the last layer.
        /// </summary>
        /// <param name="y">The required activation value.</param>
        /// <param name="costFunction">Defines the cost function.</param>
        /// <remarks>
        /// See chapter 2 BP1, chapter 3 eq 75
        /// Assumes a sigmoid activation function
        /// </remarks>
        public void CalculateDelta(float y, CostFunctionEnum costFunction)
        {
            float a = Activation;
            float d = costFunction switch
            {
                CostFunctionEnum.CrossEntropy => (a - y) / (a * (1 - a)),
                CostFunctionEnum.Quadratic => (a - y),
                _ => throw new InvalidCaseException(nameof(costFunction), costFunction, 150413),
            };
            Delta = d * ActivationDerivative(a);
        }

        #endregion
    }
}

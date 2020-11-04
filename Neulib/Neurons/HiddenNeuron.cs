using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Extensions;
using Neulib.Serializers;
using static System.Math;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents a linear neuron for use in a neural network.
    /// The activation function simply returns the sum of the weighted inputs.
    /// Methods CalculateActivation and ActivationDerivative must be overwritten in a new class for different behaviour.
    /// </summary>
    public class HiddenNeuron : Neuron, IList<Connection>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The connections with the neurons in the previous layer.
        /// </summary>
        private List<Connection> Connections { get; set; } = new List<Connection>();

        /// <summary>
        /// The number of connections of this neuron with the previous layer.
        /// </summary>
        public int Count { get => Connections.Count; }

        public bool IsReadOnly => ((ICollection<Connection>)Connections).IsReadOnly;

        /// <summary>
        /// Returns the connection with the neuron in the previous layer.
        /// </summary>
        /// <param name="index">The index of the neuron in the previous layer.</param>
        /// <returns>The connection with the neuron in the previous layer.</returns>
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
        /// The delta error value of this neuron.
        /// </summary>
        public float Delta { get; set; } = 0f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new neuron.
        /// </summary>
        public HiddenNeuron()
        {
        }

        /// <summary>
        /// Creates a new neuron from the stream.
        /// </summary>
        public HiddenNeuron(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            Bias = stream.ReadSingle();
            Sum = stream.ReadSingle();
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
        
        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return $"activation = {Activation:F3}, bias = {Bias:F3}, sum = {Sum:F3}, delta = {Delta:F3}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            HiddenNeuron value = o as HiddenNeuron ?? throw new InvalidTypeException(o, nameof(HiddenNeuron), 166408);
            Bias = value.Bias;
            Sum = value.Sum;
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

        /// <summary>
        /// Calculates Activation from Sum. Sum must have been calculated in FeedForward.
        /// </summary>
        protected virtual void CalculateActivation()
        {
            Activation = Sum;
        }

        /// <summary>
        /// Calculates the derivative of the activation function with respect to Sum.
        /// </summary>
        /// <returns>The derivative of the activation function at Activation.</returns>
        protected virtual float ActivationDerivative()
        {
            return 1f;
        }

        public override void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
            (double b, _) = random.BoxMuller(biasMagnitude);
            Bias = (float)b;
            int count = Connections.Count;
            if (count == 0) return;
            double sigma = weightMagnitude / Sqrt(count);
            for (int i = 0; i < count; i++)
            {
                (double w, _) = random.BoxMuller(sigma);
                Connections[i].Weight = (float)w;
            }
        }

        /// <summary> 
        /// Calculates the activation value of this neuron from the neurons in the previous layer, the bias, and the activation function.
        /// </summary>
        public override void FeedForward(Layer prevLayer)
        {
            int count = Connections.Count;
            float sum = Bias;
            for (int k = 0; k < count; k++)
                sum += Connections[k].Weight * prevLayer[k].Activation;
            Sum = sum;
            CalculateActivation();
        }

        public override void FeedBackward(Layer nextLayer, int j)
        {
            float d = 0f;
            int nextCount = nextLayer.Count;
            for (int k = 0; k < nextCount; k++)
            {
                HiddenNeuron nextNeuron = nextLayer[k];
                d += nextNeuron[j].Weight * nextNeuron.Delta; // wji
            }
            Delta = d * ActivationDerivative(); 
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
        public override void CalculateDelta(float y, CostFunctionEnum costFunction)
        {
            float a = Activation;
            float d = costFunction switch
            {
                CostFunctionEnum.CrossEntropy => (a - y) / (a * (1 - a)),
                CostFunctionEnum.Quadratic => (a - y),
                _ => throw new InvalidCaseException(nameof(costFunction), costFunction, 150413),
            };
            Delta = d * ActivationDerivative();
        }

        #endregion
    }
}

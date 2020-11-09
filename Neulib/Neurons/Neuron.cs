using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Extensions;
using Neulib.Serializers;
using Neulib.Numerics;
using static System.Math;
using static Neulib.Extensions.FloatExtensions;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents a linear neuron for use in a neural network.
    /// The activation function simply returns the sum of the weighted inputs.
    /// Methods CalculateActivation and ActivationDerivative must be overwritten in a new class for different behaviour.
    /// </summary>
    public class Neuron : Unit, IList<Connection>
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
        /// The activation value of this neuron calculated with the activation function and Sum.
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

        public void ForEach(Action<Connection> action)
        {
            int count = Connections.Count;
            for (int i = 0; i < count; i++)
            {
                action(Connections[i]);
            }
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
        #region Unit

        public override int CoefficientCount()
        {
            return Count + 1;
        }

        public override int SetCoefficients(Single1D coefficients, int index)
        {
            Bias = coefficients[index++];
            ForEach(connection => connection.Weight = coefficients[index++]);
            return index;
        }

        public override int GetCoefficients(Single1D coefficients, int index)
        {
            coefficients[index++] = Bias;
            ForEach(connection => coefficients[index++] = connection.Weight);
            return index;
        }

        public override void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
            (double b, _) = random.BoxMuller(biasMagnitude);
            Bias = (float)b;
            int count = Connections.Count;
            double sigma = weightMagnitude / Sqrt(count);
            for (int i = 0; i < count; i++)
            {
                (double w, _) = random.BoxMuller(sigma);
                Connections[i].Weight = (float)w;
            }
        }

        public override int CountWeight()
        {
            return Count;
        }

        public override float SumWeightSqr()
        {
            float sum = 0f;
            ForEach(connection => sum += connection.Weight.Sqr());
            return sum;
        }

        public override int AddDerivatives(Single1D derivatives, int index, float lambdaDivN)
        {
            float delta = Delta;
            // dC/dbj:
            derivatives[index++] += delta;
            // dC/dwjk + L2 regularization:
            ForEach(connection =>
                derivatives[index++] += connection.Neuron.Activation * delta + lambdaDivN * connection.Weight);
            return index;
        }

        public override void ClearConnections()
        {
            Clear();
        }

        public override void AddConnections(SingleLayer prevLayer)
        {
            prevLayer.ForEach(neuron => Add(new Connection() { Neuron = neuron }));
        }

        public override int SetActivations(Single1D activations, int index)
        {
            Activation = activations[index++];
            return index;
        }

        public override int GetActivations(Single1D activations, int index)
        {
            activations[index++] = Activation;
            return index;
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

        /// <summary> 
        /// Calculates the activation value of this neuron from the neurons in the previous layer.
        /// </summary>
        public void FeedForward()
        {
            float sum = Bias;
            ForEach(connection => sum += connection.Activation);
            Sum = sum;
            CalculateActivation();
        }

        public void FeedBackward(SingleLayer nextLayer, int j)
        {
            Delta = nextLayer.SumWeightDelta(j) * ActivationDerivative();
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
            Delta = d * ActivationDerivative();
        }


        #endregion
    }
}

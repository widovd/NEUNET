﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Serializers;
using static System.Math;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents a neuron for use in a neural network.
    /// </summary>
    public class Neuron : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

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

        /// <summary>
        /// The connections with the neurons in the previous layer.
        /// </summary>
        public List<Connection> Connections { get; private set; } = new List<Connection>();

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
        /// Creates a new neuron with a small random bias.
        /// </summary>
        public Neuron(Random random, float magnitude)
        {
            Bias = (float)(2d * random.NextDouble() - 1d) * magnitude;
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
                sum += Connections[k].Weight * prevLayer.Neurons[k].Activation;
            }
            Sum = sum; // zl
            Activation = ActivationFunction(sum); // sl
        }


        public void CalculateDelta(float y)
        {
            // Calculates deltas of the last layer
            // Chapter 2 page 9: BP1
            // Assumes a quadratic cost function: C = 0.5 * Sumj((ajL - yj)^2) ==> dC/dajL = ajL - yj
            // Assumes a sigmoid activation function
            Delta = (Activation - y) * ActivationDerivative(Activation);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using static System.Math;

namespace Neulib.NeuronsNew
{
    /// <summary>
    /// Represents a neuron for use in a neural network.
    /// </summary>
    public class NeuronStruc
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// returns the number of neurons in the previous layer, and 0 if there is no previous layer.
        /// </summary>
        public int Count { get => _connections != null ? _connections.Length : 0; }

        private readonly ConnectionStruc[] _connections;

        /// <summary>
        /// The connection of the i-th neuron in the previous layer to this neuron.
        /// </summary>
        /// <param name="i">The index of the neuron in the previous layer.</param>
        /// <returns>The connection.</returns>
        public ConnectionStruc this[int i]
        {
            get => _connections[i];
        }

        /// <summary>
        /// The bias value of this neuron.
        /// </summary>
        public float Bias { get; set; } = 0f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new neuron which is connected to a previous layer.
        /// </summary>
        /// <param name="count">The the previous layer, or null if there is no previous layer.</param>
        public NeuronStruc(LayerStruc prevLayer)
        {
            int count = prevLayer != null ? prevLayer.Count : 0;
            _connections = new ConnectionStruc[count];
            prevLayer?.ForEach((neuron, i) => _connections[i] = new ConnectionStruc(prevLayer[i]));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return $"bias = {Bias:F3}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Neuron

        public void ForEach(Action<ConnectionStruc> action)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                action(_connections[i]);
            }
        }

        public void ForEach(Action<ConnectionStruc, int> action)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                action(_connections[i], i);
            }
        }

        #endregion
    }
}

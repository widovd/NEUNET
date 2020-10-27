using Neulib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neulib.NeuronsNew
{
    /// <summary>
    /// Represents the connection between two neurons.
    /// </summary>
    public class ConnectionStruc
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The source neuron of this connection.
        /// </summary>
        public NeuronStruc Source { get; private set; }

        /// <summary>
        /// The weight value of the connection.
        /// </summary>
        public float Weight { get; set; } = 0f;

        /// <summary>
        /// The weight value times the activation of the source neuron.
        /// </summary>
        public float Product { get => Weight * Source.Activation; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public ConnectionStruc(NeuronStruc source)
        {
            Source = source ?? throw new VarNullException(nameof(source), 578306);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return $"weight = {Weight:F3}, product = {Product:F3}";
        }

        #endregion
    }
}

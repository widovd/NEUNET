using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using Neulib.Serializers;
using Neulib.MultiArrays;

namespace Neulib.Neurons
{
    public class TestNetwork
    // http://neuralnetworksanddeeplearning.com/chap1.html
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Network Network { get; private set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public TestNetwork()
        {
            Network = new Network();
            // example:
            Network.Append(new Layer(28 * 28));
            Network.Append(new Layer(15));
            Network.Append(new Layer(10));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network

        public void FeedForward(float[] xs)
        {
            Network.FeedForward(xs);
        }


        #endregion
    }
}

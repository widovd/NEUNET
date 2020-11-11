using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Neulib.Exceptions;
using Neulib.Numerics;
using Neulib.Neurons;

namespace Neulib.Serializers
{
    [Serializable]
    public class TypesDictionary : Dictionary<string, Type>
    {

        public TypesDictionary()
        {
            Add("Connection", typeof(Connection));
            Add("Unit", typeof(Unit));
            Add("Neuron", typeof(Neuron));
            Add("Sigmoid", typeof(Sigmoid));
            Add("Layer", typeof(Layer));
            Add("SingleLayer", typeof(Layer));
            Add("Network", typeof(Network));
            TestCollisions();
        }

        protected TypesDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            throw new NotImplementedException();
        }

        public static ushort GetToken(string key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            int token = 0;
            int length = bytes.Length;
            for (int i = 0; i < length; i++)
            {
                token ^= bytes[i] << (8 * (i % 2));
            }
            return (ushort)(token & 0xFFFF);
        }

        private void TestCollisions()
        {
            KeyCollection keys = Keys;
            //StringBuilder builder = new StringBuilder();
            foreach (string key1 in keys)
            {
                ushort token1 = GetToken(key1);
                if (token1 == 0)
                    throw new InvalidCodeException($"Key '{key1}' collides with the reserved key for 'null'.\nPlease change the name of this key.", 236991);
                foreach (string key2 in keys)
                {
                    if (Equals(key1, key2)) continue; // trivial, no collision
                    ushort token2 = GetToken(key2);
                    if (token1 == token2)
                        throw new InvalidCodeException($"Key '{key1}' collides with key '{key2}'.\nPlease change the name of one of these keys.", 705006);
                }
                //builder.Append($"'{key1}'={token1}");
                //builder.AppendLine();
                //Console.WriteLine($"key: '{key1}', token: {token1}");
            }
        }

    }
}

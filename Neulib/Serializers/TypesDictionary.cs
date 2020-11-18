using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Neulib.Exceptions;
using Neulib.Numerics;
using Neulib.Neurons;
using Neulib.Visuals;
using Neulib.Visuals.Arthropods;
using Neulib.Visuals.Arthropods.Myriapods;

namespace Neulib.Serializers
{
    [Serializable]
    public class TypesDictionary : Dictionary<Type, int>
    {

        public TypesDictionary()
        {
            Add(typeof(Connection), 480537);
            Add(typeof(Unit), 545117);
            Add(typeof(Neuron), 906231);
            Add(typeof(Sigmoid), 489479);
            Add(typeof(Layer), 489479);
            Add(typeof(Network), 728343);
            Add(typeof(Transform), 365737);
            Add(typeof(Visual), 861962);
            Add(typeof(World), 898392);
            Add(typeof(Segment), 742681);
            Add(typeof(Segmented), 787582);
            Add(typeof(Leg), 927638);
            Add(typeof(Limb), 271223);
            Add(typeof(Arthropod), 688396);
            Add(typeof(Myriapod), 273657);
            Add(typeof(Millipede), 669267);
        }

        protected TypesDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            throw new NotImplementedException();
        }

        public Type GetType(int token)
        {
            Type type = this.FirstOrDefault(pair => pair.Value == token).Key;
            if (type == default) // The stream contains an undefined token
                throw new InvalidValueException($"Token '{token}' is not registered in TypesDictionary", 944327);
            return type;
        }

        public int GetToken(Type type)
        {
            int token = this.FirstOrDefault(pair => pair.Key == type).Value;
            if (token == default)// The type is not defined
                throw new InvalidValueException($"Type '{type}' is not registered in TypesDictionary", 468077);
            return token;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Neulib.Serializers
{
    public interface ISerializable
    {
        void WriteToStream(Stream stream, Serializer serializer);
    }
}

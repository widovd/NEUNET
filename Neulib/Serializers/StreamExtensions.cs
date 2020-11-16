using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Numerics;
using static System.Convert;
using static System.BitConverter;

namespace Neulib.Serializers
{
    public static class StreamExtensions
    {
        public static Stream Append(this Stream destination, Stream source)
        {
            destination.Position = destination.Length;
            source.Position = 0;
            source.CopyTo(destination);
            return destination;
        }

        public static IBinarySerializable ReadValue(this Stream stream, BinarySerializer serializer)
        {
            return serializer.ReadValue(stream);
        }

        public static void WriteValue(this Stream stream, IBinarySerializable value, BinarySerializer serializer)
        {
            serializer.WriteValue(stream, value);
        }

        public static TEnum ReadEnum<TEnum>(this Stream stream) where TEnum : struct, IConvertible
        {
            return (TEnum)(object)stream.ReadInt();
            //return (TEnum)Enum.Parse(typeof(TEnum), stream.ReadString());
        }

        public static void WriteEnum(this Stream stream, Enum value)
        {
            stream.WriteInt(ToInt32(value));
            //stream.WriteString(value.ToString());
        }

        public static bool ReadBool(this Stream stream)
        {
            return ToBoolean(stream.ReadByte());
        }

        public static void WriteBool(this Stream stream, bool value)
        {
            stream.WriteByte(ToByte(value));
        }

        private static byte[] ReverseBytes(byte[] bytes)
        {
            int count = bytes.Length;
            byte[] result = new byte[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = bytes[count - 1 - i];
            }
            return result;
        }

        public static short ReadShort(this Stream stream, bool reverse = false)
        {
            const int count = sizeof(short);
            byte[] bytes = new byte[count];
            stream.Read(bytes, 0, count);
            if (reverse) bytes = ReverseBytes(bytes);
            return ToInt16(bytes, 0);
        }

        public static void WriteShort(this Stream stream, short value, bool reverse = false)
        {
            const int count = sizeof(short);
            byte[] bytes = GetBytes(value);
            int length = bytes.Length;
            if (length != count) throw new UnequalValueException(length, count, 383952);
            if (reverse) bytes = ReverseBytes(bytes);
            stream.Write(bytes, 0, count);
        }

        public static ushort ReadUshort(this Stream stream, bool reverse = false)
        {
            const int count = sizeof(ushort);
            byte[] bytes = new byte[count];
            stream.Read(bytes, 0, count);
            if (reverse) bytes = ReverseBytes(bytes);
            return ToUInt16(bytes, 0);
        }

        public static void WriteUshort(this Stream stream, ushort value, bool reverse = false)
        {
            const int count = sizeof(ushort);
            byte[] bytes = GetBytes(value);
            int length = bytes.Length;
            if (length != count) throw new UnequalValueException(length, count, 762844);
            if (reverse) bytes = ReverseBytes(bytes);
            stream.Write(bytes, 0, count);
        }

        public static int ReadInt(this Stream stream, bool reverse = false)
        {
            const int count = sizeof(int);
            byte[] bytes = new byte[count];
            stream.Read(bytes, 0, count);
            if (reverse) bytes = ReverseBytes(bytes);
            return ToInt32(bytes, 0);
        }

        public static void WriteInt(this Stream stream, int value, bool reverse = false)
        {
            const int count = sizeof(int);
            byte[] bytes = GetBytes(value);
            int length = bytes.Length;
            if (length != count) throw new UnequalValueException(length, count, 975883);
            if (reverse) bytes = ReverseBytes(bytes);
            stream.Write(bytes, 0, count);
        }

        public static long ReadLong(this Stream stream, bool reverse = false)
        {
            const int count = sizeof(long);
            byte[] bytes = new byte[count];
            stream.Read(bytes, 0, count);
            if (reverse) bytes = ReverseBytes(bytes);
            return ToInt64(bytes, 0);
        }

        public static void WriteLong(this Stream stream, long value, bool reverse = false)
        {
            const int count = sizeof(long);
            byte[] bytes = GetBytes(value);
            int length = bytes.Length;
            if (length != count) throw new UnequalValueException(length, count, 882792);
            if (reverse) bytes = ReverseBytes(bytes);
            stream.Write(bytes, 0, count);
        }

        public static float ReadSingle(this Stream stream, bool reverse = false)
        {
            const int count = sizeof(float);
            byte[] bytes = new byte[count];
            stream.Read(bytes, 0, count);
            if (reverse) bytes = ReverseBytes(bytes);
            return ToSingle(bytes, 0);
        }

        public static void WriteSingle(this Stream stream, float value, bool reverse = false)
        {
            const int count = sizeof(float);
            byte[] bytes = GetBytes(value);
            int length = bytes.Length;
            if (length != count) throw new UnequalValueException(length, count, 333545);
            if (reverse) bytes = ReverseBytes(bytes);
            stream.Write(bytes, 0, count);
        }

        public static double ReadDouble(this Stream stream, bool reverse = false)
        {
            const int count = sizeof(double);
            byte[] bytes = new byte[count];
            stream.Read(bytes, 0, count);
            if (reverse) bytes = ReverseBytes(bytes);
            return ToDouble(bytes, 0);
        }

        public static void WriteDouble(this Stream stream, double value, bool reverse = false)
        {
            const int count = sizeof(double);
            byte[] bytes = GetBytes(value);
            int length = bytes.Length;
            if (length != count) throw new UnequalValueException(length, count, 333545);
            if (reverse) bytes = ReverseBytes(bytes);
            stream.Write(bytes, 0, count);
        }

        public static Complex ReadComplex(this Stream stream, bool reverse = false)
        {
            double real = ReadDouble(stream, reverse);
            double imaginary = ReadDouble(stream, reverse);
            return new Complex(real, imaginary);
        }

        public static void WriteComplex(this Stream stream, Complex value, bool reverse = false)
        {
            stream.WriteDouble(value.Real, reverse);
            stream.WriteDouble(value.Imaginary, reverse);
        }

        public static string ReadString(this Stream stream, bool reverse = false)
        {
            int count = stream.ReadInt();
            if (count == 0) return string.Empty;
            byte[] bytes = new byte[count];
            stream.Read(bytes, 0, count);
            if (reverse) bytes = ReverseBytes(bytes);
            return Encoding.UTF8.GetString(bytes);
        }

        public static void WriteString(this Stream stream, string value, bool reverse = false)
        {
            if (string.IsNullOrEmpty(value))
            {
                stream.WriteInt(0);
                return;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            int count = bytes.Length;
            stream.WriteInt(count);
            if (reverse) bytes = ReverseBytes(bytes);
            stream.Write(bytes, 0, count);
        }



        public static Single2 ReadSingle2(this Stream stream, bool reverse = false)
        {
            float x = ReadSingle(stream, reverse);
            float y = ReadSingle(stream, reverse);
            return new Single2(x, y);
        }

        public static void WriteSingle2(this Stream stream, Single2 value, bool reverse = false)
        {
            stream.WriteSingle(value.X, reverse);
            stream.WriteSingle(value.Y, reverse);
        }

        public static Single2x2 ReadSingle2x2(this Stream stream, bool reverse = false)
        {
            float xx = ReadSingle(stream, reverse);
            float xy = ReadSingle(stream, reverse);
            float yx = ReadSingle(stream, reverse);
            float yy = ReadSingle(stream, reverse);
            return new Single2x2(xx, xy, yx, yy);
        }

        public static void WriteSingle2x2(this Stream stream, Single2x2 value, bool reverse = false)
        {
            stream.WriteSingle(value.XX, reverse);
            stream.WriteSingle(value.XY, reverse);
            stream.WriteSingle(value.YX, reverse);
            stream.WriteSingle(value.YY, reverse);
        }

        public static Single3 ReadSingle3(this Stream stream, bool reverse = false)
        {
            float x = ReadSingle(stream, reverse);
            float y = ReadSingle(stream, reverse);
            float z = ReadSingle(stream, reverse);
            return new Single3(x, y, z);
        }

        public static void WriteSingle3(this Stream stream, Single3 value, bool reverse = false)
        {
            stream.WriteSingle(value.X, reverse);
            stream.WriteSingle(value.Y, reverse);
            stream.WriteSingle(value.Z, reverse);
        }

        public static Transform ReadTransform(this Stream stream, bool reverse = false)
        {
            Single2 translation = ReadSingle2(stream, reverse);
            Single2x2 rotation = ReadSingle2x2(stream, reverse);
            return new Transform(translation, rotation);
        }

        public static void WriteTransform(this Stream stream, Transform value, bool reverse = false)
        {
            stream.WriteSingle2(value.Translation, reverse);
            stream.WriteSingle2x2(value.Rotation, reverse);
        }


    }
}

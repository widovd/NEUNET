using System;
using System.Globalization;
using System.Xml;
using Neulib.Numerics;
using Neulib.Extensions;

namespace Neulib.Serializers
{
    public static class XmlNodeExtensions
    {

        // ----------------------------------------------------------------------------------------
        #region Static properties

        public static int Digits { get; set; } = 5;

        private const char _SeparatorChar = ',';

        private static readonly char[] _SeparatorChars = new char[] { _SeparatorChar };

        public static DateTimeStyles DateTimeStyle
        {
            get { return DateTimeStyles.AssumeLocal; }
        }

        public static IFormatProvider FormatProvider { get; } = new NumberFormatInfo()
        {
            NumberDecimalSeparator = "."
        };

        #endregion
        // ----------------------------------------------------------------------------------------
        #region GetOrCreate

        public static XmlElement GetChildElement(this XmlElement element, string name)
        {
            return element[name];
        }

        public static XmlElement CreateChildElement(this XmlElement element, string name)
        {
            XmlElement childElement = element?.OwnerDocument?.CreateElement(name);
            if (childElement != null) element?.AppendChild(childElement);
            return childElement;
        }

        public static XmlElement CreateChildElement(this XmlDocument document, string name)
        {
            XmlElement childElement = document.CreateElement(name);
            document.AppendChild(childElement);
            return childElement;
        }

        public static XmlElement GetOrCreateElement(this XmlNode rootNode, string elementName)
        {
            XmlElement element = rootNode[elementName];
            if (element != null) return element;
            XmlDocument document = rootNode.OwnerDocument;
            element = document?.CreateElement(elementName);
            if (element != null) rootNode.AppendChild(element);
            return element;
        }

        public static XmlAttribute GetOrCreateAttribute(this XmlElement rootElement, string attributeName)
        {
            XmlAttribute attribute = rootElement.GetAttributeNode(attributeName);
            if (attribute != null) return attribute;
            XmlDocument document = rootElement.OwnerDocument;
            attribute = document?.CreateAttribute(attributeName);
            if (attribute != null) rootElement.SetAttributeNode(attribute);
            return attribute;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Value

        public static IXmlDocSerializable ReadValueChild(this XmlElement element, XmlDocSerializer serializer)
        {
            return serializer.ReadValueChild(element);
        }

        public static void WriteValueChild(this XmlElement element, IXmlDocSerializable serializable, XmlDocSerializer serializer)
        {
            serializer.WriteValueChild(element, serializable);
        }

        public static IXmlDocSerializable ReadValue(this XmlElement element, string name, XmlDocSerializer serializer)
        {
            return serializer.ReadValue(element, name);
        }

        public static void WriteValue(this XmlElement element, string name, IXmlDocSerializable serializable, XmlDocSerializer serializer)
        {
            serializer.WriteValue(element, name, serializable);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region String

        public static string ReadString(this XmlElement rootElement, string name, string defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            if (string.IsNullOrWhiteSpace(element.InnerText)) element.InnerText = defaultValue;
            return element.InnerText;
        }

        public static void WriteString(this XmlElement rootElement, string name, string value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.InnerText = value;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Bool

        private static bool ReadBool(this XmlNode node, bool defaultValue = false)
        {
            if (string.IsNullOrWhiteSpace(node.InnerText)) node.InnerText = defaultValue.ToString(FormatProvider);
            return node.InnerText.ReadBoolean(defaultValue);
        }

        private static void WriteBool(this XmlNode node, bool value)
        {
            node.InnerText = value.ToString(FormatProvider);
        }

        public static bool ReadBool(this XmlElement rootElement, string name, bool defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            return element.ReadBool(defaultValue);
        }

        public static void WriteBool(this XmlElement rootElement, string name, bool value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteBool(value);
        }

        // Int32

        private static int ReadInt(this XmlNode element, int defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(element.InnerText)) element.InnerText = defaultValue.ToString(FormatProvider);
            return element.InnerText.ReadInt32(FormatProvider, defaultValue);
        }

        private static void WriteInt(this XmlNode element, int value)
        {
            element.InnerText = value.ToString(FormatProvider);
        }

        public static int ReadInt(this XmlElement rootElement, string name, int defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            return element.ReadInt(defaultValue);
        }

        public static void WriteInt(this XmlElement rootElement, string name, int value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteInt(value);
        }

        public static int ReadIntAttribute(this XmlElement element, string name, int defaultValue)
        {
            return element.GetAttribute(name).ReadInt32(FormatProvider, defaultValue);
        }

        public static void WriteIntAttribute(this XmlElement element, string name, int value)
        {
            element.SetAttribute(name, value.ToString(FormatProvider));
        }


        // float

        private static float ReadFloat(this XmlNode node, float defaultValue = 0f)
        {
            if (string.IsNullOrWhiteSpace(node.InnerText)) node.InnerText = defaultValue.ToString(FormatProvider);
            return node.InnerText.ReadFloat(FormatProvider, defaultValue);
        }

        private static void WriteFloat(this XmlNode node, float value)
        {
            node.InnerText = value.ToString(FormatProvider);
        }

        public static float ReadSingle(this XmlElement rootElement, string name, float defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            return element.ReadFloat(defaultValue);
        }

        public static void WriteSingle(this XmlElement rootElement, string name, float value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteFloat(value);
        }

        // double

        private static double ReadDouble(this XmlNode node, double defaultValue)
        {
            if (string.IsNullOrWhiteSpace(node.InnerText)) node.InnerText = defaultValue.ToString(FormatProvider);
            return node.InnerText.ReadDouble(FormatProvider, defaultValue);
        }

        private static void WriteDouble(this XmlNode node, double value)
        {
            node.InnerText = value.ToString(FormatProvider);
        }

        public static double ReadDouble(this XmlElement rootElement, string name, double defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            return element.ReadDouble(defaultValue);
        }

        public static void WriteDouble(this XmlElement rootElement, string name, double value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteDouble(value);
        }

        // decimal

        private static decimal ReadDecimal(this XmlNode node, decimal defaultValue = 0m)
        {
            if (string.IsNullOrWhiteSpace(node.InnerText)) node.InnerText = defaultValue.ToString(FormatProvider);
            return node.InnerText.ReadDecimal(FormatProvider, defaultValue);
        }

        private static void WriteDecimal(this XmlNode node, decimal value)
        {
            node.InnerText = value.ToString(FormatProvider);
        }

        public static decimal ReadDecimal(this XmlElement rootElement, string name, decimal defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            return element.ReadDecimal(defaultValue);
        }

        public static void WriteDecimal(this XmlElement rootElement, string name, decimal value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteDecimal(value);
        }

        // DateTime

        private static DateTime ReadDateTime(this XmlNode node, DateTime defaultValue)
        {
            if (string.IsNullOrWhiteSpace(node.InnerText)) node.InnerText = defaultValue.ToString(FormatProvider);
            return node.InnerText.ReadDateTime(FormatProvider, defaultValue);
        }

        private static void WriteDateTime(this XmlNode node, DateTime value)
        {
            node.InnerText = value.ToString(FormatProvider);
        }

        public static DateTime ReadDateTime(this XmlElement rootElement, string name, DateTime defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            return element.ReadDateTime(defaultValue);
        }

        public static void WriteDateTime(this XmlElement rootElement, string name, DateTime value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteDateTime(value);
        }

        public static TEnum ReadEnum<TEnum>(this XmlElement rootElement, string name, TEnum defaultValue) where TEnum : struct, IConvertible
        {
            XmlElement element = rootElement[name];
            if (element == null) return defaultValue;
            string innerText = element.InnerText;
            if (string.IsNullOrWhiteSpace(innerText)) return defaultValue;
            Array values = Enum.GetValues(defaultValue.GetType());
            if (values != null)
                foreach (TEnum value in values)
                {
                    string text = value.ToString();
                    if (string.IsNullOrEmpty(text)) continue;
                    if (text == innerText) return value;
                }
            return defaultValue;
        }


        public static void WriteEnum(this XmlElement rootElement, string name, Enum value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.InnerText = value.ToString();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Double2 Double3 Single2 Single3

        private const string _XId = "X";
        private const string _YId = "Y";
        private const string _ZId = "Z";

        public static Single2 ReadSingle2(this XmlElement rootElement, string name, Single2 defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            float x = element.ReadSingle(_XId, defaultValue.X);
            float y = element.ReadSingle(_YId, defaultValue.Y);
            return new Single2(x, y);
        }

        public static Double2 ReadDouble2(this XmlElement rootElement, string name, Double2 defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            double x = element.ReadDouble(_XId, defaultValue.X);
            double y = element.ReadDouble(_YId, defaultValue.Y);
            return new Double2(x, y);
        }

        public static void WriteSingle2(this XmlElement rootElement, string name, Single2 value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteSingle(_XId, value.X);
            element.WriteSingle(_YId, value.Y);
        }

        public static void WriteDouble2(this XmlElement rootElement, string name, Double2 value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteDouble(_XId, value.X);
            element.WriteDouble(_YId, value.Y);
        }

        public static Single3 ReadSingle3(this XmlElement rootElement, string name, Single3 defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            float x = element.ReadSingle(_XId, defaultValue.X);
            float y = element.ReadSingle(_YId, defaultValue.Y);
            float z = element.ReadSingle(_ZId, defaultValue.Z);
            return new Single3(x, y, z);
        }

        public static Double3 ReadDouble3(this XmlElement rootElement, string name, Double3 defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            double x = element.ReadDouble(_XId, defaultValue.X);
            double y = element.ReadDouble(_YId, defaultValue.Y);
            double z = element.ReadDouble(_ZId, defaultValue.Z);
            return new Double3(x, y, z);
        }

        public static void WriteSingle3(this XmlElement rootElement, string name, Single3 value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteSingle(_XId, value.X);
            element.WriteSingle(_YId, value.Y);
            element.WriteSingle(_ZId, value.Z);
        }

        public static void WriteDouble3(this XmlElement rootElement, string name, Double3 value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteDouble(_XId, value.X);
            element.WriteDouble(_YId, value.Y);
            element.WriteDouble(_ZId, value.Z);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Double3x3

        private const string _XXId = "XX";
        private const string _XYId = "XY";
        private const string _XZId = "XZ";
        private const string _YXId = "YX";
        private const string _YYId = "YY";
        private const string _YZId = "YZ";
        private const string _ZXId = "ZX";
        private const string _ZYId = "ZY";
        private const string _ZZId = "ZZ";

        public static Single3x3 ReadSingle3x3(this XmlElement rootElement, string name, Single3x3 defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            float xx = element.ReadSingle(_XXId, defaultValue.XX);
            float xy = element.ReadSingle(_XYId, defaultValue.XY);
            float xz = element.ReadSingle(_XZId, defaultValue.XZ);
            float yx = element.ReadSingle(_YXId, defaultValue.YX);
            float yy = element.ReadSingle(_YYId, defaultValue.YY);
            float yz = element.ReadSingle(_YZId, defaultValue.YZ);
            float zx = element.ReadSingle(_ZXId, defaultValue.ZX);
            float zy = element.ReadSingle(_ZYId, defaultValue.ZY);
            float zz = element.ReadSingle(_ZZId, defaultValue.ZZ);
            return new Single3x3(xx, xy, xz, yx, yy, yz, zx, zy, zz);
        }


        public static Double3x3 ReadDouble3x3(this XmlElement rootElement, string name, Double3x3 defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            double xx = element.ReadDouble(_XXId, defaultValue.XX);
            double xy = element.ReadDouble(_XYId, defaultValue.XY);
            double xz = element.ReadDouble(_XZId, defaultValue.XZ);
            double yx = element.ReadDouble(_YXId, defaultValue.YX);
            double yy = element.ReadDouble(_YYId, defaultValue.YY);
            double yz = element.ReadDouble(_YZId, defaultValue.YZ);
            double zx = element.ReadDouble(_ZXId, defaultValue.ZX);
            double zy = element.ReadDouble(_ZYId, defaultValue.ZY);
            double zz = element.ReadDouble(_ZZId, defaultValue.ZZ);
            return new Double3x3(xx, xy, xz, yx, yy, yz, zx, zy, zz);
        }

        public static void WriteSingle3x3(this XmlElement rootElement, string name, Single3x3 value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteSingle(_XXId, value.XX);
            element.WriteSingle(_XYId, value.XY);
            element.WriteSingle(_XZId, value.XZ);
            element.WriteSingle(_YXId, value.YX);
            element.WriteSingle(_YYId, value.YY);
            element.WriteSingle(_YZId, value.YZ);
            element.WriteSingle(_ZXId, value.ZX);
            element.WriteSingle(_ZYId, value.ZY);
            element.WriteSingle(_ZZId, value.ZZ);
        }

        public static void WriteDouble3x3(this XmlElement rootElement, string name, Double3x3 value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteDouble(_XXId, value.XX);
            element.WriteDouble(_XYId, value.XY);
            element.WriteDouble(_XZId, value.XZ);
            element.WriteDouble(_YXId, value.YX);
            element.WriteDouble(_YYId, value.YY);
            element.WriteDouble(_YZId, value.YZ);
            element.WriteDouble(_ZXId, value.ZX);
            element.WriteDouble(_ZYId, value.ZY);
            element.WriteDouble(_ZZId, value.ZZ);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Chart2DBounds

        private const string _XLoId = "XLo";
        private const string _XHiId = "XHi";
        private const string _YLoId = "YLo";
        private const string _YHiId = "YHi";

        public static ParamBounds ReadDouble2Bounds(this XmlElement rootElement, string name, ParamBounds defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            double XLo = element.ReadDouble(_XLoId, defaultValue.XLo);
            double XHi = element.ReadDouble(_XHiId, defaultValue.XHi);
            double YLo = element.ReadDouble(_YLoId, defaultValue.YLo);
            double YHi = element.ReadDouble(_YHiId, defaultValue.YHi);
            return new ParamBounds(XLo, XHi, YLo, YHi);
        }

        public static void WriteDouble2Bounds(this XmlElement rootElement, string name, ParamBounds value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteDouble(_XLoId, value.XLo);
            element.WriteDouble(_XHiId, value.XHi);
            element.WriteDouble(_YLoId, value.YLo);
            element.WriteDouble(_YHiId, value.YHi);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Double2

        private const string _UId = "U";
        private const string _VId = "V";

        public static Double2 ReadParamCoord(this XmlElement rootElement, string name, Double2 defaultValue)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            double u = element.ReadDouble(_UId, defaultValue.X);
            double v = element.ReadDouble(_VId, defaultValue.Y);
            return new Double2(u, v);
        }

        public static void WriteParamCoord(this XmlElement rootElement, string name, Double2 value)
        {
            XmlElement element = rootElement.GetOrCreateElement(name);
            element.WriteDouble(_UId, value.X);
            element.WriteDouble(_VId, value.Y);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region XmlAttribute

        // https://www.w3schools.com/xml/xml_dtd_el_vs_attr.asp
        // In XML, there are no rules about when to use attributes, and when to use child elements. But:
        // If you use attributes as containers for data, you end up with documents that are difficult to read and maintain.
        // Try to use elements to describe data. Use attributes only to provide information that is not relevant to the data.
        // What I am trying to say here is that metadata (data about data) should be stored as attributes, 
        // and that data itself should be stored as elements.

        // text
        public static string ReadString(this XmlAttribute attribute, string defaultText = "")
        {
            string text = attribute.Value;
            if (string.IsNullOrWhiteSpace(text)) text = defaultText;
            return text;
        }

        public static void WriteString(this XmlAttribute attribute, string text)
        {
            attribute.Value = text;
        }

        // Example:
        public static string AttributeToText(this XmlElement rootElement, string attributeName, string defaultValue)
        {
            XmlAttribute attribute = rootElement.GetOrCreateAttribute(attributeName);
            return attribute.ReadString(defaultValue);
        }

        public static void AttributeFromText(this XmlElement rootElement, string attributeName, string value)
        {
            XmlAttribute attribute = rootElement.GetOrCreateAttribute(attributeName);
            attribute.WriteString(value);
        }

        // Boolean

        public static bool ReadBoolean(this XmlAttribute attribute, bool defaultValue = false)
        {
            return attribute.Value.ReadBoolean(defaultValue);
        }

        public static void WriteBoolean(this XmlAttribute attribute, bool value)
        {
            attribute.Value = value.ToString(FormatProvider);
        }

        // Int32

        public static int ReadInt(this XmlAttribute attribute, int defaultValue = 0)
        {
            return attribute.Value.ReadInt32(FormatProvider, defaultValue);
        }

        public static void WriteInt(this XmlAttribute attribute, int value)
        {
            attribute.Value = value.ToString(FormatProvider);
        }

        // Single

        public static float ReadFloat(this XmlAttribute attribute, float defaultValue = 0f)
        {
            return attribute.Value.ReadFloat(FormatProvider, defaultValue);
        }

        public static void WriteFloat(this XmlAttribute attribute, float value)
        {
            attribute.Value = value.ToString(FormatProvider);
        }

        // Double

        public static double ReadDouble(this XmlAttribute attribute, double defaultValue = 0d)
        {
            return attribute.Value.ReadDouble(FormatProvider, defaultValue);
        }

        public static void WriteDouble(this XmlAttribute attribute, double value)
        {
            attribute.Value = value.ToString(FormatProvider);
        }

        // Decimal

        public static decimal ReadDecimal(this XmlAttribute attribute, decimal defaultValue = 0m)
        {
            return attribute.Value.ReadDecimal(FormatProvider, defaultValue);
        }

        public static void WriteDecimal(this XmlAttribute attribute, decimal value)
        {
            attribute.Value = value.ToString(FormatProvider);
        }

        // DateTime

        public static DateTime ReadDateTime(this XmlAttribute attribute, DateTime defaultValue)
        {
            return attribute.Value.ReadDateTime(FormatProvider, defaultValue);
        }

        public static void WriteDateTime(this XmlAttribute attribute, DateTime value)
        {
            attribute.Value = value.ToString(FormatProvider);
        }

        #endregion
    }

}

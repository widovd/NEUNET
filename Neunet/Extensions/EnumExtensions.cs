using System;
using System.ComponentModel;
using System.Reflection;
using Neulib.Exceptions;

namespace Neunet.Extensions
{
    public static class ExposedEnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }

        public static string GetShowName(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (Attributes.ShowNameAttribute)fieldInfo.GetCustomAttribute(typeof(Attributes.ShowNameAttribute));
            return attribute.Text;
        }

        public static string GetXmlText(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (Attributes.XmlTextAttribute)fieldInfo.GetCustomAttribute(typeof(Attributes.XmlTextAttribute));
            return attribute != null ? attribute.XmlText : fieldInfo.Name;
        }

        public static string GetXmlText<TEnum>(this TEnum value) where TEnum : struct, IConvertible
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (Attributes.XmlTextAttribute)fieldInfo.GetCustomAttribute(typeof(Attributes.XmlTextAttribute));
            return attribute != null ? attribute.XmlText : fieldInfo.Name;
        }

        public static int ToInt<TEnum>(this TEnum value) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum) throw new InvalidTypeException(value, "Enum", 529118);
            return (int)(IConvertible)value;
        }

    }

}

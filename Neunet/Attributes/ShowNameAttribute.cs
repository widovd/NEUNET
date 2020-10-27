using System;
using System.Reflection;

namespace Neunet.Attributes
{
    public sealed class ShowNameAttribute : Attribute
    {
        public string Text { get; private set; }

        public ShowNameAttribute(string text) : base()
        {
            Text = text;
        }
        public static string GetShowName(object value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (Attributes.ShowNameAttribute)fieldInfo.GetCustomAttribute(typeof(Attributes.ShowNameAttribute));
            return attribute.Text;
        }

    }


}

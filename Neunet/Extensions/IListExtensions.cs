using System;
using System.Collections;

namespace Neunet.Extensions
{
    public static class IListExtensions
    {

        public static void PopulateEnum(this IList items, Type enumType)
        {
            items.Clear();
            Array values = Enum.GetValues(enumType);
            foreach (Enum value in values)
            {
                string text = value.GetShowName();
                if (string.IsNullOrEmpty(text)) continue;
                items.Add(text);
            }
        }

    }
}

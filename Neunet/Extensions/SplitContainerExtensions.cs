using System.Windows.Forms;
using System.Xml;
using Neulib.Extensions;
using Neulib.Serializers;

namespace Neunet.Extensions
{
    public static class SplitContainerExtensions
    {
        public static void SetSplitterDistance(this SplitContainer splitContainer, int splitterDistance)
        {
            if (splitterDistance > 0) splitContainer.SplitterDistance = splitterDistance.Clip(1, splitContainer.Width - 1);
        }

        public static void SetSplitterDistance(this SplitContainer splitContainer, XmlElement rootElement, string name)
        {
            SetSplitterDistance(splitContainer, rootElement.ReadInt(name, splitContainer.SplitterDistance));
        }
    }

}

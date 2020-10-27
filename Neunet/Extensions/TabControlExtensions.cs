using System.Windows.Forms;
using System.Xml;
using Neulib.Extensions;
using Neulib.Serializers;

namespace Neunet.Extensions
{
    public static class TabControlExtensions
    {
        public static void SetSelectedIndex(this TabControl tabControl, int selectedIndex)
        {
            if (selectedIndex > 0) tabControl.SelectedIndex = selectedIndex.Clip(0, tabControl.TabCount - 1);
        }

        public static void SetSelectedIndex(this TabControl tabControl, XmlElement rootElement, string name)
        {
            SetSelectedIndex(tabControl, rootElement.ReadInt(name, tabControl.SelectedIndex));
        }
    }

}

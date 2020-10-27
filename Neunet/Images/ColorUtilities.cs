using System.Drawing;
using System.ComponentModel;
using Neunet.Attributes;
using static System.Convert;

namespace Neunet.Images
{

    public enum ColorGradientEnum
    {
        [ShowName("0"), XmlText("Gradient0"), Description("Gradient 0")] Gradient0,
        [ShowName("1"), XmlText("Gradient1"), Description("Gradient 1")] Gradient1,
        [ShowName("2"), XmlText("Gradient2"), Description("Gradient 2")] Gradient2,
        [ShowName("3"), XmlText("Gradient3"), Description("Gradient 3")] Gradient3,
        [ShowName("4"), XmlText("Gradient4"), Description("Gradient 4")] Gradient4,
        [ShowName("5"), XmlText("Gradient5"), Description("Gradient 5")] Gradient5,
        [ShowName("6"), XmlText("Gradient6"), Description("Gradient 6")] Gradient6,
        [ShowName("7"), XmlText("Gradient7"), Description("Gradient 7")] Gradient7,
        [ShowName("8"), XmlText("Gradient8"), Description("Gradient 8")] Gradient8,
    }

    public static class ColorUtilities
    {
        /// <summary>
        /// Returns a dark color to be used with light background colors.
        /// </summary>
        /// <param name="index">The index of the color.</param>
        /// <returns>A dark color.</returns>
        public static Color GetDarkColor(int index)
        {
            return (index & 15) switch
            {
                0 => Color.DarkBlue,
                1 => Color.DarkGreen,
                2 => Color.DarkRed,
                3 => Color.DarkCyan,
                4 => Color.DarkOrange,
                5 => Color.DarkMagenta,
                6 => Color.DarkKhaki,
                7 => Color.DarkOliveGreen,
                8 => Color.DarkGoldenrod,
                9 => Color.DarkViolet,
                10 => Color.DarkOrchid,
                11 => Color.DarkTurquoise,
                12 => Color.DarkSalmon,
                13 => Color.DarkSeaGreen,
                14 => Color.DarkSlateBlue,
                15 => Color.DarkSlateGray,
                _ => Color.DarkGray,
            };
        }

        public static Color GetColor(float z, params Color[] Colors)
        {
            z = 1f - z; // Because feedback from users: Color gradients must match LightTools colors.
            int n = Colors.Length - 1;
            z *= n;
            if (z < 0d) return Color.DarkSlateBlue;
            for (int i = 0; i < n; i++)
            {
                if (z <= 1f)
                {
                    Color c1 = Colors[i], c2 = Colors[i + 1];
                    float Red = (1f - z) * c1.R + z * c2.R;
                    float Green = (1f - z) * c1.G + z * c2.G;
                    float Blue = (1f - z) * c1.B + z * c2.B;
                    return Color.FromArgb((int)Red, (int)Green, (int)Blue);
                }
                z -= 1f;
            }
            return _higherThanOneColor;
        }

        private static readonly Color _lowerThanZeroColor = Color.White;
        private static readonly Color _higherThanOneColor = Color.Red;

        private static readonly Color _c01 = Color.White;
        private static readonly Color _c02 = Color.White;

        private static readonly Color _c11 = Color.FromArgb(0x0060C060); // green
        private static readonly Color _c12 = Color.FromArgb(0x00FFF080); // yellow
        private static readonly Color _c13 = Color.FromArgb(0x00F06060); // red
        private static readonly Color _c14 = Color.White;

        private static readonly Color _c21 = Color.FromArgb(0x0060C060); // green
        private static readonly Color _c22 = Color.FromArgb(0x0080FFF0); // cyan
        private static readonly Color _c23 = Color.FromArgb(0x006060F0); // blue
        private static readonly Color _c24 = Color.White;

        private static readonly Color _c31 = Color.Red;
        private static readonly Color _c32 = Color.Yellow;
        private static readonly Color _c33 = Color.Green;
        private static readonly Color _c34 = Color.Cyan;
        private static readonly Color _c35 = Color.Blue;
        private static readonly Color _c36 = Color.Violet;

        private static readonly Color _c41 = Color.White;
        private static readonly Color _c42 = Color.Black;
        private static readonly Color _c43 = Color.Red;

        private static readonly Color _c51 = Color.White;
        private static readonly Color _c52 = Color.Black;
        private static readonly Color _c53 = Color.Yellow;

        public static Color GetColor(float z, ColorGradientEnum colorGradient, bool inverse)
        {
            if (inverse) z = 1f - z;
            switch (colorGradient)
            {
                case ColorGradientEnum.Gradient0:
                    return GetColor(z, Color.White, Color.Black);
                case ColorGradientEnum.Gradient1:
                    return GetColor(z, _c11, _c12, _c13);
                case ColorGradientEnum.Gradient2:
                    return GetColor(z, _c01, _c11, _c12, _c13, _c02);
                case ColorGradientEnum.Gradient3:
                    return GetColor(z, _c21, _c22, _c23);
                case ColorGradientEnum.Gradient4:
                    return GetColor(z, _c01, _c21, _c22, _c23, _c02);
                case ColorGradientEnum.Gradient5:
                    return GetColor(z, _c31, _c32, _c33, _c34, _c35, _c36); // ARGB
                case ColorGradientEnum.Gradient6:
                    return GetColor(z, _c01, _c31, _c32, _c33, _c34, _c35, _c36, _c02); // ARGB
                case ColorGradientEnum.Gradient7:
                    return GetColor(z, _c41, _c42, _c43, _c42, _c41, _c42, _c43, _c42, _c41, _c42, _c43, _c42, _c41, _c42, _c43, _c42, _c41, _c42, _c43, _c42, _c41); // ARGB
                case ColorGradientEnum.Gradient8:
                    return GetColor(z, _c51, _c52, _c53, _c52, _c51); // ARGB

                default:
                    return Color.Black;
            }
        }

        public static Color Mix(Color c1, Color c2)
        {

            int r = (ToInt32(c1.R) + ToInt32(c2.R)) / 2;
            int g = (ToInt32(c1.G) + ToInt32(c2.G)) / 2;
            int b = (ToInt32(c1.B) + ToInt32(c2.B)) / 2;
            return Color.FromArgb(r, g, b);

        }
    }
}

using System.Globalization;
using UnityEngine;

namespace Hieki.Utils
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts <see cref="Color"/> to Html format
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToHtml(this Color color)
        {
            return "#" + ColorUtility.ToHtmlStringRGB(color);
        }

        /// <summary>
        /// Returns a new Color with given red.
        /// </summary>
        public static Color R(this Color color, float r)
        {
            return new Color(r, color.g, color.b, color.a);
        }

        /// <summary>
        /// Returns a new Color with given green.
        /// </summary>
        public static Color G(this Color color, float g)
        {
            return new Color(color.r, g, color.b, color.a);
        }

        /// <summary>
        /// Returns a new Color with given blue.
        /// </summary>
        public static Color B(this Color color, float b)
        {
            return new Color(color.r, color.g, b, color.a);
        }

        /// <summary>
        /// Returns a new Color with given alpha.
        /// </summary>
        public static Color A(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static bool IsBrighter(this Color color, Color other)
        {
            Color.RGBToHSV(color, out float _ /*hue1*/, out float _ /*saturation1*/, out float brightness1);
            Color.RGBToHSV(other, out float _ /*hue2*/, out float _ /*saturation2*/, out float brightness2);
            return brightness1 > brightness2;
        }

        // Note that Color32 and Color implictly convert to each other. You may pass a Color object to this method without first casting it.
        public static string ToHex(this Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }

        public static Color FromHex(string hex)
        {
            hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
            hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
            byte a = 255;//assume fully visible unless specified in hex
            byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            //Only use alpha if the string has enough characters
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
            }
            return new Color32(r, g, b, a);
        }
    }
}

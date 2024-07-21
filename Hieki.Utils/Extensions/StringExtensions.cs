namespace Hieki.Utils
{
    public static class StringExtensions
    {
        public static float ReadTime(this string s)
        {
            float length = s.Length;
            float averageWordCount = length / 4;

            int minutes = (int)(averageWordCount / 200);
            float seconds = (averageWordCount / 200 - minutes) * 60f;
            return minutes * 60 + seconds;
        }

        #region Tags
        public static string AddBoldTag(this string text)
        {
            return text.AddTag("b");
        }

        public static string AddItalicTag(this string text)
        {
            return text.AddTag("i");
        }

        public static string AddSizeTag(this string text, int size)
        {
            return text.AddTag("size", size);
        }

        public static string AddColorTag(this string text, string colorName)
        {
            return text.AddTag("color", colorName);
        }

        private static string AddTag(this string text, string tagName)
        {
            return $"<{tagName}>{text}</{tagName}>";
        }

        private static string AddTag(this string text, string tagName, object value1)
        {
            return $"<{tagName}=\"{value1}\">{text}</{tagName}>";
        }
        #endregion
    }
}

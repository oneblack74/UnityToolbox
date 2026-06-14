using UnityEngine;

namespace oneBlack74.UnityToolbox.Core
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
            => string.IsNullOrEmpty(s);

        public static bool IsNullOrWhiteSpace(this string s)
            => string.IsNullOrWhiteSpace(s);

        // Parses a hex string to Color — supports #RGB, #RRGGBB, #RRGGBBAA
        public static Color ToColor(this string hex)
        {
            if (ColorUtility.TryParseHtmlString(hex, out Color color))
                return color;

            Debug.LogWarning($"[StringExtensions] Invalid hex color: {hex}");
            return Color.white;
        }
    }
}

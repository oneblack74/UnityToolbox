using UnityEngine;

namespace oneBlack74.UnityToolbox.Core
{
    public static class ColorExtensions
    {
        public static Color WithAlpha(this Color color, float alpha)
            => new(color.r, color.g, color.b, alpha);
        
        public static Color WithRed(this Color color, float red)
            => new(red, color.g, color.b, color.a);
        
        public static Color WithGreen(this Color color, float green)
            => new(color.r, green, color.b, color.a);
        
        public static Color WithBlue(this Color color, float blue)
            => new(color.r, color.g, blue, color.a);
    }
}

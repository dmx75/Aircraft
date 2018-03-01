using System;
using Android.Graphics;

namespace App1.Helpers
{
    public static class ColorHelper
    {
        public static Color PercentToColor(float percent)
        {
            if (percent < 0 || percent > 1) { return Color.Black; }

            int r, g;
            if (percent < 0.5)
            {
                r = 255;
                g = (int)(255 * percent / 0.5);  //closer to 0.5, closer to yellow (255,255,0)
            }
            else
            {
                g = 255;
                r = 255 - (int)(255 * (percent - 0.5) / 0.5); //closer to 1.0, closer to green (0,255,0)
            }
            return Color.Argb(255, r, g, 0);
        }

        public static Color GetBlendedColor(double percentage)
        {
            if (percentage < 50)
                return Interpolate(Color.Red, Color.Yellow, percentage / 50.0);
            return Interpolate(Color.Yellow, Color.Lime, (percentage - 50) / 50.0);
        }

        private static Color Interpolate(Color color1, Color color2, double fraction)
        {
            double r = Interpolate(color1.R, color2.R, fraction);
            double g = Interpolate(color1.G, color2.G, fraction);
            double b = Interpolate(color1.B, color2.B, fraction);
            return Color.Argb(255, (int)Math.Round(r), (int)Math.Round(g), (int)Math.Round(b));
        }

        private static double Interpolate(double d1, double d2, double fraction)
        {
            return d1 + (d2 - d1) * fraction;
        }
    }
}
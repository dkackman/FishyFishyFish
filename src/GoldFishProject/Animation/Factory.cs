using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;

namespace FishTank.Animation
{
    static class Factory
    {
        private const int FRAMECOUNT = 20;

        public static IEnumerable<Tuple<Bitmap, Bitmap>> GetFrames(string color, int width)
        {
            using (var left = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream($"FishTank.Resources.{color}.Left.png")))
            using (var right = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream($"FishTank.Resources.{color}.Right.png")))
            {
                Debug.Assert(left.Width == right.Width);
                Debug.Assert(left.Height == right.Height);

                var bitmaps = new List<Tuple<Bitmap, Bitmap>>();
                for (int i = 0; i < FRAMECOUNT; i++)
                {
                    var leftFrame = left.ExtractFrame(i, width);
                    var rightFrame = right.ExtractFrame(i, width);

                    bitmaps.Add(Tuple.Create(leftFrame, rightFrame));
                }

                return bitmaps.AsReadOnly();
            }
        }

        private static Bitmap ExtractFrame(this Bitmap frames, int offset, int width)
        {
            var bitmap = new Bitmap(width, frames.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(frames, new Rectangle(0, 0, width, frames.Height), new Rectangle(width * offset, 0, width, frames.Height), GraphicsUnit.Pixel);
            }

            return bitmap;
        }
    }
}

using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace merge.images
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var s in new string[] { "l", "r" })
            {
                for (int k = 4; k <= 7; k++)
                {
                    for (int c = 1; c <= k; c++)
                    {
                        string kc = $@"D:\MEGAsync\dataset front features\k{k}\c{c}-{s}";
                        string[] files = Directory.EnumerateFiles(kc, "*.*", SearchOption.AllDirectories).Where(w => w.EndsWith(".png")).ToArray();

                        var dst = new Mat(files[0], ImreadModes.AnyColor);

                        for (int i = 1; i < files.Length; i++)
                        {
                            var src = new Mat(files[i], ImreadModes.AnyColor);

                            Cv2.BitwiseAnd(dst, src, dst);
                            //Cv2.AddWeighted(dst, 0.5d, src, 0.5d, 0d, dst);
                        }

                        Cv2.Flip(dst, dst, FlipMode.X);

                        dst.SaveImage($@"D:\MEGAsync\dataset front features\k{k}\c{c}-{s}.png");
                    }
                }
            }
        }
    }
}

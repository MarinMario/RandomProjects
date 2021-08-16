using System;
using System.Drawing;
using System.Threading;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            var img = new Bitmap("Content/copac.png");
            var imgData = new Color[img.Width * img.Height];
            for (var y = 0; y < img.Height; y++)
                for (var x = 0; x < img.Width; x++)
                {
                    var pixel = img.GetPixel(x, y);
                    imgData[y * img.Width + x] = pixel;
                }
            var speed = 50;

            ////line sound
            //var line = 0;
            //for (var y = 0; y < img.Height; y++)
            //{
            //    var lineAvg = 0;
            //    for (var x = 0; x < img.Width; x++)
            //    {
            //        var p = imgData[y * img.Width + x];
            //        var avg = (p.R + p.G + p.B) / 3;
            //        lineAvg += avg;
            //    }
            //    lineAvg /= img.Width;
            //    var f = lineAvg * 50 + 100;
            //    Console.WriteLine($"{f} {line}");
            //    line += 1;
            //    Console.Beep(f, speed);
            //    Thread.Sleep(1);
            //}

            //pixel sound
            var line = 0;
            for (var y = 0; y < imgData.Length; y++)
            {

                var p = imgData[y];
                var avg = (p.R + p.G + p.B) / 3;
                var f = avg * 100 + 100;
                Console.WriteLine($"{f} {line}");
                line += 1;
                //console.beep(f, speed);
                Console.Beep(f, speed);
            }

        }
    }
}
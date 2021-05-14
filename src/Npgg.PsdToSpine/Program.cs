using Ntreev.Library.Psd;
using System;
using System.Drawing.Imaging;

namespace Npgg.PsdToSpine
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "sample1.psd";
            PsdDocument document = PsdDocument.Create(filename);


            foreach (var layer in document.Childs)
            {
                var bmap = layer.GetBitmap();
                bmap.Save($"{layer.Name}.png", ImageFormat.Png);

                Console.WriteLine($"{layer.Name}.png saved");
            }

        }
    }
}

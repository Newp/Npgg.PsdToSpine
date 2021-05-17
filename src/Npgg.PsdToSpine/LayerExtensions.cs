
using Ntreev.Library.Psd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Drawing.Imaging;
using System.Drawing;

namespace Npgg.PsdToSpine
{
    public static class LayerExtensions
    {
        public static Bitmap GetBitmap(this IPsdLayer imageSource)
        {
            if (imageSource.HasImage)
                return imageSource.GetLayerImage();

            if (imageSource.Childs.Length == 1)
                return imageSource.Childs.First().GetLayerImage();

            if (imageSource.Childs.Length > 1)
                return imageSource.GetGroupImage();

            throw new NotImplementedException();
        }

        public static Bitmap GetGroupImage(this IPsdLayer imageSource)
        {
            var childs = imageSource.Childs;
            var top = childs.Min(layer => layer.Top);
            var left = childs.Min(layer => layer.Left);

            var width = childs.Max(layer => layer.Right - left);
            var height = childs.Max(layer => layer.Bottom - top);

            var result = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(result);
            foreach (var layer in childs)
            {
                g.DrawImage(layer.GetBitmap(), new Point( layer.Left - left, layer.Top - top) );
            }
            
            return result;
        }

        public static Bitmap GetLayerImage(this IPsdLayer imageSource)
        {
            if (imageSource.HasImage == false)
                return null;

            byte[] data = imageSource.MergeChannels();
            var channelCount = imageSource.Channels.Length;
            var pitch = imageSource.Width * channelCount;
            var w = imageSource.Width;
            var h = imageSource.Height;

            //var format = channelCount == 3 ? TextureFormat.RGB24 : TextureFormat.ARGB32;
            bool is4chan = channelCount == 4;
            var bitmap = new Bitmap(w, h);

            for (var y = h - 1; y >= 0; --y)
            {
                for (var x = 0; x < pitch; x += channelCount)
                {
                    var n = x + y * pitch;

                    var b = data[n++];
                    var g = data[n++];
                    var r = data[n++];
                    var a = is4chan ? (byte)System.Math.Round(data[n++] / 255f * imageSource.Opacity * 255f) : (byte)System.Math.Round(imageSource.Opacity * 255f);


                    var color = Color.FromArgb(a, r, g, b);

                    bitmap.SetPixel(x / channelCount, y, color);
                }
            }

            return bitmap;
        }
    }
}
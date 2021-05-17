﻿
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
            var bitmaps = imageSource.Childs.Select(layer => layer.GetBitmap()).Reverse();

            var result = bitmaps.Aggregate((bmp1, bmp2) => MergedBitmaps(bmp1, bmp2));
            return result;
        }
        private static Bitmap MergedBitmaps(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap result = new Bitmap(Math.Max(bmp1.Width, bmp2.Width),
                                       Math.Max(bmp1.Height, bmp2.Height));
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp2, Point.Empty);
                g.DrawImage(bmp1, Point.Empty);
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
            
            bitmap.Save( $"debug_{imageSource.Name}.png", ImageFormat.Png);

            return bitmap;
        }
    }
}
﻿using Newtonsoft.Json;
using Ntreev.Library.Psd;
using System;
using System.Linq;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Npgg.PsdToSpine
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "sample1.psd";
            PsdDocument document = PsdDocument.Create(filename);

            var bottom = document.Childs.Max(layer => layer.Top + layer.Height);

            var attachments = document.Childs.Reverse().Select(layer => new AttachmentInfo()
            {
                Height = layer.Height,
                Width = layer.Width,
                X = layer.Left + (layer.Width / 2) - (document.Width /2),
                Y = -layer.Top - (layer.Height / 2) + bottom,
                Name = layer.Name,
                Path = $"imgs/{layer.Name}",
                Bitmap = layer.GetBitmap(),
            });

            foreach (var layer in attachments)
            {
                layer.Bitmap.Save($"{layer.Path}.png", ImageFormat.Png);
                Console.WriteLine($"{layer.Path}.png saved");
            }

            var result = new
            {
                bones = new[] { new { name = "root" } },
                animations = new { animation = new { } },
                //
                skeleton = new SkeletonInfo()
                {
                    Hash = "b1SvfaW5KKe2QFddu2nXQdb/nj0",
                    Spine="3.8.99",
                    Width = 0,
                    Height = 0,
                    //Width = document.Width,
                    //Height = document.Height,
                    Images = string.Empty,
                    Audio = string.Empty,
                },
                slots = attachments.Select(attachment =>new SlotInfo
                {
                    Name = attachment .Name,
                    Attachment = attachment .Path,
                    Bone = "root",
                }).ToArray(),

                skins = new SkinInfo[]
                {
                    new SkinInfo()
                    {
                        Name ="default",
                        Attachments = attachments.ToDictionary(
                            attachment=>attachment.Name,
                            attachment=> new Dictionary<string, AttachmentInfo>()
                            {
                                { attachment.Path, attachment }
                            }
                        )
                    }
                }
            };

            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText(filename + ".json", json, new UTF8Encoding(false));
        }
    }
}

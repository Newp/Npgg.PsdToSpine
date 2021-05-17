using Newtonsoft.Json;
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



            var attachments = document.Childs.Reverse().Select(layer => new AttachmentInfo()
            {
                Height = layer.Height,
                Width = layer.Width,
                X = layer.Left,
                Y = layer.Top,
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
                    Width = 400,
                    Height = 500,
                    Images = string.Empty,
                    Audio = string.Empty,
                },
                slots = attachments.Select(layer=>new SlotInfo
                {
                    Name = layer.Path,
                    Attachment = layer.Path,
                    Bone = "root",
                }).ToArray(),

                skins = new SkinInfo[]
                {
                    new SkinInfo()
                    {
                        Name ="default",
                        Attachments = attachments.ToDictionary(
                            attachment=>attachment.Path,
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

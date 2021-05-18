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
    partial class Program
    {
        static readonly Encoding encoding = new UTF8Encoding(false);
        static void Main(string[] args)
        {
            string filename = "sample1.psd";
            string boneMapPath = "boneMap.json";

            var boneMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(boneMapPath, encoding));

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
                //bones = 
                //    
                //    .Append(new BoneInfo { Name = "root" }),

                bones = new[] { new BoneInfo { Name = "root" } }
                    .Concat(attachments.Select(attachment => new BoneInfo { 
                        Name = attachment.Name, 
                        X = attachment.X, 
                        Y=attachment.Y, 
                        Parent = "root"// boneMap.ContainsKey(attachment.Name) ? boneMap[attachment.Name] : "root" 
                    })
                ),

                animations = new { animation = new { } },
                //
                skeleton = new SkeletonInfo()
                {
                    Hash = "b1SvfaW5KKe2QFddu2nXQdb/nj0",
                    Spine="3.8.99",
                    Width = 0,
                    Height = 0,
                    Images = string.Empty,
                    Audio = string.Empty,
                },
                slots = attachments.Select(attachment =>new SlotInfo
                {
                    Name = attachment.Name,
                    Attachment = attachment.Path,
                    Bone = "root"
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
            File.WriteAllText(filename + ".json", json, encoding);
        }
    }

}

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
        static readonly string ImageOuputPath = "imgs";
        static void Main(string[] args)
        {
            string filename = "sample1.psd";
            string boneMapPath = "boneMap.json";

            var boneMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(boneMapPath, encoding));

            PsdDocument document = PsdDocument.Create(filename);

            var bottom = document.Childs.Max(layer => layer.Top + layer.Height);

            var layers = document.Childs.Reverse().Select(layer => new LayerInfo()
            {
                Height = layer.Height,
                Width = layer.Width,
                X = layer.Left + (layer.Width / 2) - (document.Width /2),
                Y = -layer.Top - (layer.Height / 2) + bottom,
                Name = layer.Name,
                Path = $"{ImageOuputPath }/{layer.Name}",
                Bitmap = layer.GetBitmap(),
            });


            if(Directory.Exists(ImageOuputPath) == false)
            {
                Directory.CreateDirectory(ImageOuputPath);
            }

            foreach (var layer in layers)
            {
                layer.Bitmap.Save($"{layer.Path}.png", ImageFormat.Png);
                Console.WriteLine($"{layer.Path}.png saved");
            }

            var result = new
            {
                bones = BoneInfo.CreateBoneInfos(boneMap, layers),

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
                slots = layers.Select(layer =>new SlotInfo
                {
                    Name = layer.Name,
                    Attachment = layer.Path,
                    Bone = layer.Name//"root"
                }).Reverse(),

                skins = new SkinInfo[]
                {
                    new SkinInfo()
                    {
                        Name ="default",
                        Attachments = layers.ToDictionary(
                            layer=>layer.Name,
                            layer=> new Dictionary<string, AttachmentInfo>()
                            {
                                { layer.Path, layer.GetAttachment() }
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

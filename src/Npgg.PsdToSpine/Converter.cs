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
    public class Converter
    {
        static readonly string ImageOuputPath = "imgs";

        public static SpineOption Convert(string fileName, Dictionary<string, string> boneMap)
        {
            var groupName = "정면";
            PsdDocument document = PsdDocument.Create(fileName);

            var group = document.Childs.Single(child => child.Name == groupName);

            var bottom = group.Childs.Max(layer => layer.Top + layer.Height);
            
            var layers = group.Childs.Reverse().Select(layer => new LayerInfo()
            {
                Height = layer.Height,
                Width = layer.Width,
                X = layer.Left + (layer.Width / 2) - (document.Width /2),
                Y = -layer.Top - (layer.Height / 2) + bottom,
                Name = layer.Name,
                Path = $"{ImageOuputPath }/{layer.Name}",
                Bitmap = layer.GetBitmap(),
            }).ToArray();


            if(Directory.Exists(ImageOuputPath) == false)
            {
                Directory.CreateDirectory(ImageOuputPath);
            }

            foreach (var layer in layers)
            {
                layer.Bitmap.Save($"{layer.Path}.png", ImageFormat.Png);
                Console.WriteLine($"{layer.Path}.png saved");
            }

            var result = new SpineOption
            {
                Bones = BoneInfo.CreateBoneInfos(boneMap, layers),

                //Animations = new { animation = new { } },
                //
                Skeleton = new SkeletonInfo()
                {
                    Hash = "b1SvfaW5KKe2QFddu2nXQdb/nj0",
                    Spine="3.8.99",
                    Width = 0,
                    Height = 0,
                    Images = string.Empty,
                    Audio = string.Empty,
                },
                Slots = layers.Select(layer =>new SlotInfo
                {
                    Name = layer.Name,
                    Attachment = layer.Path,
                    Bone = layer.Name//"root"
                }).Reverse().ToArray(),

                Skins = new SkinInfo[]
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

            return result;
        }
    }
}

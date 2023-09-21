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

        public static SpineOption Convert(string fileName, Configuration configuration , string imageOutput)
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
                Path = $"{ImageOuputPath}/{layer.Name}",
                Bitmap = layer.GetBitmap(),
            }).ToArray();

            var outputFullPath = $"{imageOutput}/{ImageOuputPath}";

            if(Directory.Exists(ImageOuputPath) == false)
            {
                Directory.CreateDirectory(ImageOuputPath);
            }

            foreach (var layer in layers)
            {
                layer.Bitmap.Save($"{imageOutput}/{layer.Path}.png", ImageFormat.Png);
                Console.WriteLine($"{imageOutput}/{layer.Path}.png saved");
            }

            

            var slots = configuration.SlotOrder.Select(slotName=> layers.Single(layer=>layer.Name == slotName))
                .Select(layer => new SlotInfo
                {
                    Name = layer.Name,
                    Attachment = layer.Path,
                    Bone = layer.Name//"root"
                }).Reverse().ToArray();

            var result = new SpineOption
            {
                Bones = BoneInfo.CreateBoneInfos(configuration.BoneMap, layers),
                //Animations = new { animation = new { } },
                //
                Skeleton = new SkeletonInfo()
                {
                    Hash = "b1SvfaW5KKe2QFddu2nXQdb/nj0",
                    Spine="4.1.23",
                    Width = 0,
                    Height = 0,
                    Images = string.Empty,
                    Audio = string.Empty,
                },
                Slots = slots,

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

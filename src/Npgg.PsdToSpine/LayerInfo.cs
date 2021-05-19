using System.Drawing;

namespace Npgg.PsdToSpine
{
    public class LayerInfo
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public BoneInfo Bone { get; set; }

        public Bitmap Bitmap { get; set; }


        public long X { get; set; }

        public long Y { get; set; }

        public long Width { get; set; }

        public long Height { get; set; }

        public AttachmentInfo GetAttachment() => new AttachmentInfo()
        {
            X = 0,//this.X,
            Y = 0,//this.Y,
            Height = this.Height,
            Width = this.Width,
        };
    }
}

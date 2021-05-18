using Newtonsoft.Json;
using System.Drawing;

namespace Npgg.PsdToSpine
{
    public class AttachmentInfo
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public string Path { get; set; }

        [JsonIgnore]
        public BoneInfo Bone { get; set; }

        [JsonIgnore]
        public Bitmap Bitmap { get; set; }


        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }
    }
}

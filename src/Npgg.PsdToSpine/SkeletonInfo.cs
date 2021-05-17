using Newtonsoft.Json;

namespace Npgg.PsdToSpine
{
    public class SkeletonInfo
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("spine")]
        public string Spine { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("images")]
        public string Images { get; set; }

        [JsonProperty("audio")]
        public string Audio { get; set; }
    }
}

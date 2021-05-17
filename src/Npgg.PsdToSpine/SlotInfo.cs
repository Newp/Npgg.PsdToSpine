using Newtonsoft.Json;

namespace Npgg.PsdToSpine
{
    public class SlotInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("bone")]
        public string Bone { get; set; }

        [JsonProperty("attachment")]
        public string Attachment { get; set; }
    }
}

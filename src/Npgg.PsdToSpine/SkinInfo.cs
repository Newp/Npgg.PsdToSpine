using Newtonsoft.Json;
using System.Collections.Generic;

namespace Npgg.PsdToSpine
{
    public class SkinInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("attachments")]
        public Dictionary<string, Dictionary<string, AttachmentInfo>> Attachments { get; set; }
    }
}

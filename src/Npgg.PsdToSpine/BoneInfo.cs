using Newtonsoft.Json;

namespace Npgg.PsdToSpine
{
 
    public class BoneInfo
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Parent { get; set; }

        [JsonProperty("length", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Length { get; set; }

        [JsonProperty("rotation", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Rotation { get; set; }


        [JsonProperty("x", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double X { get; set; }


        [JsonProperty("y", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Y { get; set; }
    }
    

}

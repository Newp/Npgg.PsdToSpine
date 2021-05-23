using Newtonsoft.Json;

namespace Npgg.PsdToSpine
{
    public class SpineOption
    {
        [JsonProperty("bones")]
        public BoneInfo[] Bones { get; set; }


        [JsonProperty("skeleton")]
        public SkeletonInfo Skeleton { get; set; }

        [JsonProperty("slots")]
        public SlotInfo[] Slots { get; set; }

        [JsonProperty("skins")]
        public SkinInfo[] Skins { get; set; }
    }
}

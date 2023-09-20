using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [JsonProperty("transform", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Transform { get; set; }

        
        public static BoneInfo[] CreateBoneInfos(Dictionary<string ,string> boneMap, IEnumerable<LayerInfo> attachmentInfos)
        {
            var addedBones = attachmentInfos.Where(item => boneMap.ContainsKey(item.Name)).ToDictionary(item => item.Name);

            var result = addedBones
                .Values
                .Select(layer => new BoneInfo
                {
                    Name = layer.Name,
                    X = layer.X,
                    Y = layer.Y,
                    Parent = boneMap.ContainsKey(layer.Name) ? boneMap[layer.Name] : "root",
                })
                .ToList();

            foreach (var bone in result)
            {
                if (bone.Parent == "root") continue;

                var parent = addedBones[bone.Parent];

                bone.X -= parent.X;
                bone.Y -= parent.Y;
            }

            result.Insert(0, new BoneInfo { Name = "root" });

            return result.ToArray();
        }
    }

    

}

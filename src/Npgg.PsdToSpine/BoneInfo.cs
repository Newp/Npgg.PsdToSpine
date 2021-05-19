using Newtonsoft.Json;
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

        
        public static BoneInfo[] CreateBoneInfos(Dictionary<string ,string> boneMap, IEnumerable<AttachmentInfo> attachmentInfos)
        {
            //var addedBones = new Dictionary<string, BoneInfo>(); // 원래는 string 본 이름과 BoneInfo로 할 예정이었다.
            //addedBones.Add("root", new BoneInfo { Name = "root" });
            //var queue = new Queue<string>(attachmentInfos.Select(item => item.Name));

            //while (queue.Count > 0)
            //{
            //    var attachment = queue.Dequeue();
            //    if (boneMap.TryGetValue(attachment, out var parentBone) == false)
            //        parentBone = "root";

            //        addedBones.Add(attachment);
            //    else
            //        queue.Enqueue(attachment);
            //}

            //var result = addedBones
            //    .Select(name => attachmentInfos.First(attachmentInfo => attachmentInfo.Name == name))
            //    .Select(attachment => new BoneInfo
            //    {
            //        Name = attachment.Name,
            //        X = attachment.X,
            //        Y = attachment.Y,
            //        Parent = boneMap.ContainsKey(attachment.Name) ? boneMap[attachment.Name] : "root",
            //        //Transform = "onlyTranslation",
            //    })
            //    .ToList();

            //result.Insert(0, new BoneInfo { Name = "root" });

            //return result;

            var addedBones = new Dictionary<string, AttachmentInfo>(); // 원래는 string 본 이름과 BoneInfo로 할 예정이었다.

            var queue = new Queue<AttachmentInfo>(attachmentInfos);

            while (queue.Count > 0)
            {
                var attachment = queue.Dequeue();

                if (boneMap.TryGetValue(attachment.Name, out var parentBone) == false)
                {
                    addedBones.Add(attachment.Name, attachment);
                    continue;
                }
                
                if(addedBones.ContainsKey(parentBone))
                    addedBones.Add(attachment.Name, attachment);
                else
                    queue.Enqueue(attachment);
            }

            var result = addedBones
                .Values
                .Select(attachment => new BoneInfo
                {
                    Name = attachment.Name,
                    X = attachment.X,
                    Y = attachment.Y,
                    Parent = boneMap.ContainsKey(attachment.Name) ? boneMap[attachment.Name] : "root",
                    //Transform = "onlyTranslation",
                })
                .ToList();

            result.Insert(0, new BoneInfo { Name = "root" });

            return result.ToArray();
        }
    }

    

}

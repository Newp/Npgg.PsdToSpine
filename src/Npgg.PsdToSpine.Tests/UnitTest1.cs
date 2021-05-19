using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace Npgg.PsdToSpine.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var attachments = new List<string>
            {
                "Eyes",
                "Body",
                "Head",
                "Foot"
            };

            var boneMap = new Dictionary<string, string>()
            {
                { "Eyes", "Head" },
                { "Foot", "Body" },
                { "Head", "Body" }
            };

            var addedBones = new List<string>(); // 원래는 string 본 이름과 BoneInfo로 할 예정이었다.
            addedBones.Add("Root");

            for (int i = 0; attachments.Count > 0; i++)
            {
                if (i >= attachments.Count)
                    i = 0;

                var attachment = attachments[i];
                if (boneMap.TryGetValue(attachment, out var parentBone) == false)
                    parentBone = "Root";

                if (addedBones.Contains(parentBone) == false)
                    continue;

                addedBones.Add(attachment);
                attachments.Remove(attachment);
                i--;
            }

            var resultMessage = string.Join(',', addedBones);

            Assert.StartsWith("Root,Body,Head", resultMessage);
        }

        [Fact]
        public void Test2()
        {
            var attachments = new List<string>
            {
                "Eyes",
                "Body",
                "Head",
                "Foot"
            };

            var boneMap = new Dictionary<string, string>()
            {
                { "Eyes", "Head" },
                { "Foot", "Body" },
                { "Head", "Body" }
            };

            var addedBones = new List<string>(); // 원래는 string 본 이름과 BoneInfo로 할 예정이었다.
            addedBones.Add("Root");

            var queue = new Queue<string>(attachments);

            while (queue.Count > 0)
            {
                var attachment = queue.Dequeue();
                if (boneMap.TryGetValue(attachment, out var parentBone) == false)
                    parentBone = "Root";

                if (addedBones.Contains(parentBone))
                    addedBones.Add(attachment);
                else
                    queue.Enqueue(attachment);
            }

            var resultMessage = string.Join(',', addedBones);

            Assert.StartsWith("Root,Body,Head", resultMessage);
        }
    }
}

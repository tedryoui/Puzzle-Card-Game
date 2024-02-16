using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DefaultNamespace.Illustration_Part_Demo
{
    public class IllustrationStager
    {
        private class IllustrationPartData_Stage
        {
            public IllustrationData.IllustrationPartData Data;
            public AttachableIllustrationPart Attachable;
            public IllustrationPart Part;
        }

        private IllustrationPartData_Stage[] stages;

        public IllustrationStager(IllustrationData data)
        {
            stages = GenerateStages(data).ToArray();
        }

        private List<IllustrationPartData_Stage> GenerateStages(IllustrationData data)
        {
            var s = new List<IllustrationPartData_Stage>();

            foreach (var dataPart in data.partDates)
            {
                s.Add(new IllustrationPartData_Stage()
                {
                    Data = dataPart,
                    Attachable = null,
                    Part = null
                });
            }

            return s;
        }

        public void SetTo(AttachableIllustrationPart attachable, IllustrationPart part)
        {
            var stage = stages.FirstOrDefault(x => x.Data.x.Equals(attachable.X) && x.Data.y.Equals(attachable.Y));
            if (stage == null) return;

            stage.Attachable ??= attachable;
            stage.Part = part;
        }

        public bool IsCompleted()
        {
            foreach (var stage in stages)
            {
                if (stage.Attachable == null || stage.Part == null) return false;
                if (!stage.Attachable.CompareIndicesTo(stage.Part)) return false;
            }

            return true;
        }
    }
}
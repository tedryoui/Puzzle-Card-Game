using System;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Illustration_Part_Demo
{
    [CreateAssetMenu(fileName = "Illustration Data", menuName = "Illustration/Data", order = 0)]
    public class IllustrationData : ScriptableObject
    {
        [Serializable]
        public class IllustrationPartData
        {
            public int x;
            public int y;

            public Sprite image;
        }
        
        public int sizeX;
        public int sizeY;

        public int partSize;
        public int partsSpace;

        public int draggableSize;

        public Sprite illustration;
        public IllustrationPartData[] partDates;

        public bool IsValid()
        {
            if (partDates.Length > sizeX * sizeY) return false;
            
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (!partDates.Any(data => data.x >= 0 && data.x < sizeX)) return false;
                }
            }

            return true;
        }
    }
}
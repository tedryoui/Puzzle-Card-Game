using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Illustration_Part_Demo
{
    public class IllustrationSizeAdjust : MonoBehaviour
    {
        public enum PartType { Main, Additional }

        [SerializeField] private PartType _initialPartType;

        private void Start()
        {
            UpdateSize(_initialPartType);
        }

        public void UpdateSize(PartType type)
        {
            switch (type)
            {
                case PartType.Main:
                    (transform as RectTransform).sizeDelta = Vector2.one * IllustrationCore.Instance.Data.partSize;
                    break;
                case PartType.Additional:
                    (transform as RectTransform).sizeDelta = Vector2.one * IllustrationCore.Instance.Data.draggableSize;
                    break;
            }
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Illustration_Part_Demo
{
    public class IllustrationPreview : MonoBehaviour
    {
        [SerializeField] private IllustrationData _data;
        [SerializeField] private Image _image;

        private void Start()
        {
            _image.sprite = _data.illustration;
        }

        public void StartIllustrationPuzzle()
        {
            IllustrationCore.Instance.SetData(_data);
        }
    }
}
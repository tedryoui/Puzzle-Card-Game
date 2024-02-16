using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace.Illustration_Part_Demo
{
    public class PivotsGroupLayout : MonoBehaviour
    {
        [SerializeField] private RectTransform[] _pivots;

        private RectTransform[] _objects;

        [Space(10)]
        public UnityEvent rebuilded;

        private void CacheObjects()
        {
            _objects = GetComponentsInChildren<RectTransform>().ToArray();
        }

        public void RebuildLayout()
        {
            if (_objects == null || _objects.Length != transform.childCount)
                CacheObjects();

            Rebuild();
        }

        private void Rebuild()
        {
            for (var i = 0; i < _objects.Length; i++)
            {
                var o = _objects[i];
                var t = _pivots[i % _pivots.Length];

                o.position = t.position;
            }
            
            rebuilded?.Invoke();
        }
    }
}
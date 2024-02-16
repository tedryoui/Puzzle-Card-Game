using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Illustration_Part_Demo
{
    public class ShuffleChildPositions : MonoBehaviour
    {
        private RectTransform[] _children;

        public void Shuffle()
        {
            if (_children == null || _children.Length != transform.childCount)
                CacheObject();

            // Shuffle algorithm
            
            var positions = new List<Vector3>();
            for (int i = 0; i < _children.Length; i++) positions.Add(_children[i].transform.position);

            var rnd = new  System.Random();
            positions = positions.OrderBy(x => rnd.Next()).ToList();

            for (int i = 0; i < _children.Length; i++) _children[i].transform.position = positions[i];
        }

        private void CacheObject()
        {
            _children = GetComponentsInChildren<RectTransform>().ToArray();
        }
    }
}
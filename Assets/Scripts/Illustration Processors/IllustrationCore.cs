using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Illustration_Part_Demo
{
    public class IllustrationCore : MonoBehaviour
    {
        #region Singleton

        private static IllustrationCore _instance;
        
        public static IllustrationCore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType(typeof(IllustrationCore), FindObjectsInactive.Include) as IllustrationCore;

                    if (_instance == null)
                        throw new Exception($"Scene does not contain any object of type {nameof(IllustrationCore)}");
                }

                return _instance;
            }
        }

        private void SingletonAwake()
        {
            _instance = this;
        }

        private void OnDestroy()
        {
            _instance = null;
        }

        #endregion
        
        [SerializeField] private IllustrationData _data;
        private IllustrationStager _stager;

        [Space(10)]
        public UnityEvent compelted;
        public UnityEvent started;

        public IllustrationData Data => _data;
        
        private void Awake()
        {
            SetData(_data);
        }

        public void SetData(IllustrationData data)
        {
            _data = data;
            _stager = new IllustrationStager(_data);
            
            started?.Invoke();
        }

        public void Set(AttachableIllustrationPart attachable, IllustrationPart part)
        {
            _stager.SetTo(attachable, part);
        }

        public void CheckForCompletion()
        {
            if (_stager.IsCompleted()) compelted?.Invoke();
        }
    }
}
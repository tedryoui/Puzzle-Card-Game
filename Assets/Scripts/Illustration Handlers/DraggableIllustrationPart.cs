using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.Illustration_Part_Demo
{
    public class DraggableIllustrationPart : IllustrationPart, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _yOffset;
        [SerializeField] private IllustrationSizeAdjust _sizeAdjust;
        
        private Vector3 _initialPosition = default;
        private bool _isMoving = false;

        #region Drag

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isMoving)
            {
                // Cache initial position if its not being cached before
                if (_initialPosition == default)
                    _initialPosition = transform.position;
                
                // Adjust size to normal
                _sizeAdjust.UpdateSize(IllustrationSizeAdjust.PartType.Main);
                
                transform.SetAsLastSibling();
                _isMoving = true;
            }
        }

        private void Update()
        {
            if (_isMoving)
            {
                // Adjust position with current mouse position and offset
                transform.position = Input.mousePosition + Vector3.up * _yOffset;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isMoving)
            {
                _isMoving = false;

                // Replace eventData position with same but including offset
                eventData.position = eventData.position + Vector2.up * _yOffset;

                // Raycast under eventData data
                var list = new List<RaycastResult>(); 
                EventSystem.current.RaycastAll(eventData, list);

                if (list.Count <= 1) return;

                // Check raycast result except first element, cause it equals to [this.gameObject] object
                CheckUnder(list.Skip(1).ToArray());
                
                // Check core for completion
                IllustrationCore.Instance.CheckForCompletion();
            }
        }

        #endregion

        private void CheckUnder(RaycastResult[] raycasts, int index = 0)
        {
            // Get object to check
            var primary = raycasts[index];
            
            if (primary.gameObject.TryGetComponent(typeof(AttachableIllustrationPart), out var attachableIllustrationPart))
            {
                // Check Attachable Illustration Part
                var component = attachableIllustrationPart as AttachableIllustrationPart;

                if (component.isAttachable)
                {
                    SetPosition(component.transform.position);
                    
                    // Register stager data in illustration core
                    IllustrationCore.Instance.Set(component, this);
                    
                    // Adjust size to draggable
                    _sizeAdjust.UpdateSize(IllustrationSizeAdjust.PartType.Main);}
                else
                {
                    SetPosition();
                    
                    // Register stager data in illustration core
                    IllustrationCore.Instance.Set(component, null);
                    
                    // Adjust size to draggable
                    _sizeAdjust.UpdateSize(IllustrationSizeAdjust.PartType.Additional);
                }
            }
            else if (primary.gameObject.TryGetComponent(typeof(IllustrationPart), out var illustrationPart))
            {
                // Check Illustration Part
                var component = illustrationPart as IllustrationPart;

                if (component is DraggableIllustrationPart draggingComponent)
                {
                    draggingComponent.SetPosition();
                    draggingComponent._sizeAdjust.UpdateSize(IllustrationSizeAdjust.PartType.Additional);
                }
                
                CheckUnder(raycasts, index + 1);
            } else
            {
                // Reset position to initial value
                SetPosition();
                
                // Adjust size to draggable
                _sizeAdjust.UpdateSize(IllustrationSizeAdjust.PartType.Additional);
            }
        }

        private void SetPosition(Vector3 position = default)
        {
            transform.position = position == default ? _initialPosition : position;
        }
    }
}
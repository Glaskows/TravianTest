using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Travian.Input
{
    public class DragInputHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public UnityEvent<float> OnHorizontalDrag;
        public UnityEvent<float> OnVerticalDrag;
        
        private Vector2 lastPos, pos;
        private bool draggingStarted;
        
        private void Start()
        {
            draggingStarted = false;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            draggingStarted = true;
            
            lastPos = eventData.pressPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (draggingStarted)
            {
                pos = eventData.position;
                Vector2 difference = pos - lastPos;

                if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
                {
                    OnHorizontalDrag?.Invoke(difference.x);
                }
                else
                {
                    OnVerticalDrag?.Invoke(difference.y);
                }

                lastPos = pos;
            }

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            draggingStarted = false;
        }
    }   
}
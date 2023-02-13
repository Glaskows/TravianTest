using Travian.Math;
using Travian.WorldMap;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Travian.Input
{
    public class TerrainClickHandler : MonoBehaviour
    {
        public UnityEvent<HexCoordinate> OnTerrainClicked;
        
        [SerializeField] float maxRayDistance = 100;
        [SerializeField] LayerMask collidableLayers;
        [SerializeField] private Camera worldCamera;

        void Update()
        {
            bool isPressed = UnityEngine.Input.GetMouseButtonDown(0);
            bool isTouched = UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began;
            
            if (isPressed || isTouched)
            {
                Vector3 screenPosition;
                if (isPressed)
                {
                    screenPosition = UnityEngine.Input.mousePosition;
                }
                else
                {
                    // portrait mode doesn't rotate coordinates
                    Vector2 touchPosition = UnityEngine.Input.GetTouch(0).position;
                    screenPosition = new Vector3(touchPosition.x, touchPosition.y, 0f);
                }
                
                Ray ray = worldCamera.ScreenPointToRay(screenPosition);
                if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, collidableLayers))
                {
                    Vector3 hitPos = hit.collider.transform.position;
                    HexCoordinate mapHexCoordinate = Tile.Vector2Coordinate(new Vector2(hitPos.x, hitPos.z));
                    
                    OnTerrainClicked?.Invoke(mapHexCoordinate);
                }
            }
        }
    }
}
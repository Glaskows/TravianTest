using JetBrains.Annotations;
using Travian.Math;
using Travian.WorldMap;
using UnityEngine;

namespace Travian.Renderer
{
   public class CursorRenderer : MonoBehaviour
   {
      [SerializeField] private GameObject arrowPrefab;

      private Map map;
      private GameObject arrow;

      public void OnEnable()
      {
         arrow = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity, transform);
         arrow.SetActive(false);
      }

      public void SetMap(Map map)
      {
         this.map = map;
      }

      [UsedImplicitly]
      public void OnCursorOn(HexCoordinate coordinate)
      {
         arrow.SetActive(true);
         arrow.transform.position = map.GetWorldTilePosition(coordinate);
      }

      [UsedImplicitly]
      public void OnCursorOff()
      {
         arrow.SetActive(false);
      }
   }
}
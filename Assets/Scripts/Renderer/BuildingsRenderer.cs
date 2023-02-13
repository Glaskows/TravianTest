using System.Collections.Generic;
using Travian.Math;
using Travian.WorldMap;
using UnityEngine;

namespace Travian.Renderer
{
    public class BuildingsRenderer : MonoBehaviour
    {
        [SerializeField] private BuildingTileSet tileSet;
        private List<Tile3d> buildings;
        private Map map;

        public void Start()
        {
            buildings = new List<Tile3d>();
        }
        
        public void SetMap(Map map)
        {
            this.map = map;
        }
        
        public void CreateBuilding(HexCoordinate coordinate, BuildingType type)
        {
            // only one building per coordinate
            int idx = buildings.FindIndex(tile => tile.Coordinate == coordinate);
            if (idx >= 0)
                return;
            
            Vector3 worldPos = map.GetWorldTilePosition(coordinate);
            GameObject buildingPrefab = tileSet.GetTilePrefab(type);
            GameObject buildingGo = Instantiate(buildingPrefab, worldPos, Quaternion.identity, transform);
            buildingGo.name = $"{type} {coordinate.ToString()}";
            
            buildings.Add(new Tile3d(coordinate, buildingGo));
        }

        public void DestroyBuilding(HexCoordinate coordinate)
        {
            int idx = buildings.FindIndex(tile => tile.Coordinate == coordinate);
            if (idx < 0)
                return;
            
            Destroy(buildings[idx].GameObject);
            buildings.RemoveAt(idx);
        }
    }
}
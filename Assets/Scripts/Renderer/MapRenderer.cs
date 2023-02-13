using System.Collections.Generic;
using Travian.Math;
using Travian.WorldMap;
using UnityEngine;

namespace Travian.Renderer
{
    public class MapRenderer : MonoBehaviour
    {
        [SerializeField] private MapTileSet tileSet;
        private Map map;
        private List<Tile3d> tiles;
        
        public void SetMapAndRender(Map map)
        {
            this.map = map;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            GenerateTiles3d();
        }

        public void SetTileVisibility(HexCoordinate coordinate, bool isVisible)
        {
            int idx = tiles.FindIndex(tile => tile.Coordinate == coordinate);
            if (idx < 0)
                return;

            tiles[idx].GameObject.SetActive(isVisible);
        }

        private void GenerateTiles3d()
        {
            tiles = new List<Tile3d>();
            
            foreach (Tile tile in map.Tiles)
            {
                int height = tile.Height;
                
                // fill with dirt below the tile
                Vector3 worldPos = tile.GetWorldPosition();
                if (tile.Type != TileType.Water)
                {
                    for (int h = -1; h < height; ++h)
                    {
                        float y = h * Tile.Thickness;
                        GameObject fillGo = Instantiate(tileSet.FillingTile, new Vector3(worldPos.x, y, worldPos.z),
                            Quaternion.identity, transform);
                        fillGo.name = $"Fill {tile.Coordinate.ToString()}.{h}";
                    }
                }

                if (tile.Type == TileType.Water)
                {
                    worldPos.y += 0.1f;
                }
                else if (tile.Type == TileType.Sand)
                {
                    worldPos.y -= 0.05f;
                }
                
                GameObject tilePrefab = tileSet.GetTilePrefab(tile.Type);
                GameObject tileGo = Instantiate(tilePrefab, worldPos, Quaternion.identity, transform);
                tileGo.name = $"{tile.Type} {tile.Coordinate.ToString()}.{height}";

                // water never changes, no need to keep a reference
                if (tile.Type != TileType.Water)
                {
                    tiles.Add(new Tile3d(tile.Coordinate, tileGo));
                }
            }
        }
    }
}
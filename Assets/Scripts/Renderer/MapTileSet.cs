using Travian.WorldMap;
using UnityEngine;

namespace Travian.Renderer
{
    [CreateAssetMenu(fileName = "MapTileSet", menuName = "ScriptableObjects/TileSet/MapTileSet")]
    public class MapTileSet : TileSet<TileType>
    {
        public GameObject FillingTile;
    }
}
using Travian.WorldMap;
using UnityEngine;

namespace Travian.Renderer
{
    [CreateAssetMenu(fileName = "BuildingTileSet", menuName = "ScriptableObjects/TileSet/BuildingTileSet")]
    public class BuildingTileSet : TileSet<BuildingType>
    {
    }
}
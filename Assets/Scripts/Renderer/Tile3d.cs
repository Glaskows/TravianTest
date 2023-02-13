using Travian.Math;
using UnityEngine;

namespace Travian.Renderer
{
    public struct Tile3d
    {
        public readonly HexCoordinate Coordinate;
        public readonly GameObject GameObject;

        public Tile3d(HexCoordinate coordinate, GameObject tile3d)
        {
            Coordinate = coordinate;
            GameObject = tile3d;
        }
    }
}
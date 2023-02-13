using System;
using Travian.Math;

namespace Travian.WorldMap
{
    [Serializable]
    public struct Building
    {
        public readonly HexCoordinate Position;
        public readonly BuildingType Type;

        public Building(HexCoordinate position, BuildingType type)
        {
            Position = position;
            Type = type;
        }
    }
}
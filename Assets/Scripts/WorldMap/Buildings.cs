using System;
using System.Collections.Generic;
using Travian.Math;

namespace Travian.WorldMap
{
    [Serializable]
    public class Buildings
    {
        private List<Building> buildings;

        public Buildings()
        {
            buildings = new List<Building>(32);
        }

        public Building? GetBuildingAt(HexCoordinate coordinate)
        {
            foreach (Building building in buildings)
            {
                if (building.Position == coordinate)
                {
                    return building;
                }
            }

            return null;
        }

        public bool DestroyBuildingAt(HexCoordinate coordinate)
        {
            int idx = buildings.FindIndex(building => building.Position == coordinate);
            if (idx < 0)
                return false;

            buildings.RemoveAt(idx);
            return true;
        }

        public bool CreateBuildingAt(HexCoordinate coordinate, BuildingType type)
        {
            if (GetBuildingAt(coordinate).HasValue)
                return false;

            buildings.Add(new Building(coordinate, type));
            return true;
        }
    }
}
using Travian.Math;
using Travian.Resource;

namespace Travian.WorldMap
{
    public class BuildingConstructor
    {
        private Buildings buildings;
        private readonly BuildingsConfig buildingsConfig;
        private Map map;
        private Resources resources;

        public BuildingConstructor(Map map, BuildingsConfig buildingsConfig, Resources resources)
        {
            this.map = map;
            this.buildingsConfig = buildingsConfig;
            this.resources = resources;
            
            buildings = new Buildings();
        }
        
        public bool CanCreateBuilding(HexCoordinate coordinate, BuildingType type)
        {
            bool isTileEmpty = IsTileEmpty(coordinate);
            bool canBuildHere = CanTileHoldBuilding(coordinate, type);
            bool hasEnoughResources = HasEnoughResourcesToBuild(type);
                
            return canBuildHere && isTileEmpty && hasEnoughResources;
        }

        public bool CanDestroyBuilding(HexCoordinate coordinate)
        {
            Building? building = buildings.GetBuildingAt(coordinate);
            bool buildingExist = building.HasValue;

            return buildingExist;
        }
        
        public void CreateBuilding(HexCoordinate coordinate, BuildingType type)
        {
            // safety check: could remove if feeling dangerous
            if (!CanCreateBuilding(coordinate, type))
                return;

            foreach (var cost in buildingsConfig.GetBuildingCost(type))
            {
                resources.SubstractResource(cost.Type, cost.Amount);
            }
            
            buildings.CreateBuildingAt(coordinate, type);
        }

        public void DestroyBuilding(HexCoordinate coordinate)
        {
            Building? building = buildings.GetBuildingAt(coordinate);
            bool buildingExist = building.HasValue;
            
            if (!buildingExist)
                return;

            foreach (var cost in buildingsConfig.GetBuildingSalvage(building.Value.Type))
            {
                resources.AddResource(cost.Type, cost.Amount);
            }
            
            buildings.DestroyBuildingAt(coordinate);
        }

        public bool HasEnoughResourcesToBuild(BuildingType type)
        {
            Resource.Resource[] costs = buildingsConfig.GetBuildingCost(type);
            foreach (var cost in costs)
            {
                if (resources.GetResourceAmount(cost.Type) < cost.Amount)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Stub: only checks for grass, buildings could have an array of allowed tile types
        /// </summary>
        public bool CanTileHoldBuilding(HexCoordinate coordinate, BuildingType type)
        {
            Tile? tile = map.GetTileAt(coordinate);
            return tile?.Type == TileType.Grass;
        }

        public bool IsTileEmpty(HexCoordinate coordinate)
        {
            Building? building = buildings.GetBuildingAt(coordinate);
            return !building.HasValue;
        }
    }
}
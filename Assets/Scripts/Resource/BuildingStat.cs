using System;
using Travian.WorldMap;

namespace Travian.Resource
{
    [Serializable]
    public struct BuildingStats
    {
        public BuildingType BuildingType;
        public Resource[] Costs;
        public Resource[] Salvage;
    }
}
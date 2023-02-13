using System;
using Travian.WorldMap;
using UnityEngine;

namespace Travian.Resource
{
    [CreateAssetMenu(fileName = "BuildingsConfig", menuName = "ScriptableObjects/Resources/BuildingsConfig")]
    public class BuildingsConfig: ScriptableObject
    {
        [SerializeField] private BuildingStats[] stats;

        private static readonly Resource[] NoResources = { };

        public Resource[] GetBuildingCost(BuildingType type)
        {
            int idx = Array.FindIndex(stats, stat => stat.BuildingType == type);
            return idx < 0 ? NoResources : stats[idx].Costs;
        }

        public Resource[] GetBuildingSalvage(BuildingType type)
        {
            int idx = Array.FindIndex(stats, stat => stat.BuildingType == type);
            return idx < 0 ? NoResources : stats[idx].Salvage;
        }
    }
}
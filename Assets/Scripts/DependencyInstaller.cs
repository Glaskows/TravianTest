using Travian.Input;
using Travian.Renderer;
using Travian.Resource;
using Travian.WorldMap;
using UnityEngine;
using Resources = Travian.Resource.Resources;

namespace Travian
{
    public class DependencyInstaller : MonoBehaviour
    {
        [SerializeField] private MapRenderer mapRenderer;
        [SerializeField] private CursorRenderer uiRenderer;
        [SerializeField] private ConstructionHandler constructionHandler;
        [SerializeField] private BuildingsRenderer buildingsRenderer;
        [SerializeField] private ResourcesConfig initialResources;
        [SerializeField] private BuildingsConfig buildingsConfig;
        [SerializeField] private ResourcesRenderer resourcesRenderer;
        
        private Map map;
        private BuildingConstructor buildingConstructor;
        private Resources resources;
        
        public void Start()
        {
            map = new Map(15, 100);
            
            resources = new Resources(initialResources.Resources);
            resources.OnResourceChange += resourcesRenderer.UpdateResourceValue;
            resources.ForceUpdate();
            
            buildingConstructor = new BuildingConstructor(map, buildingsConfig, resources);
            
            mapRenderer.SetMapAndRender(map);
            buildingsRenderer.SetMap(map);
            uiRenderer.SetMap(map);
            constructionHandler.SetBuilder(buildingConstructor);
        }
    }

}
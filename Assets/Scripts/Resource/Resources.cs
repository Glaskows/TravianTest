using System;
using System.Collections.Generic;

namespace Travian.Resource
{
    public class Resources
    {
        public event Action<Resource> OnResourceChange;
        
        private List<Resource> resources;

        public Resources(Resource[] initialResources)
        {
            resources = new List<Resource>();
            
            foreach (Resource resource in initialResources)
            {
                resources.Add(new Resource(resource.Type, resource.Amount));
            }
        }

        public void ForceUpdate()
        {
            foreach (Resource resource in resources)
            {
                OnResourceChange?.Invoke(resource);
            }
        }

        public void AddResource(ResourceType type, int amount)
        {
            Resource resource = resources.Find(resource => resource.Type == type);
            if (resource == null)
            {
                resource = new Resource(type);
                resources.Add(resource);
            }
            resource.Add(amount);
            OnResourceChange?.Invoke(resource);
        }
        
        /// <summary>
        /// Substract this type of resource, returns false if not enough resources are available
        /// </summary>
        public bool SubstractResource(ResourceType type, int amount)
        {
            Resource resource = resources.Find(r => r.Type == type);
            if (resource == null || resource.Amount < amount)
            {
                return false;
            }
            resource.Remove(amount);
            OnResourceChange?.Invoke(resource);
            return true;
        }

        public int GetResourceAmount(ResourceType type)
        {
            Resource resource = resources.Find(r => r.Type == type);
            if (resource == null)
            {
                return 0;
            }
            return resource.Amount;
        }
    }
}
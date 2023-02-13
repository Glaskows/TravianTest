using System;
using UnityEngine;

namespace Travian.Resource
{
    [Serializable]
    public class Resource
    {
        [SerializeField] private ResourceType type;
        [SerializeField] private int amount;

        public int Amount => amount;
        public ResourceType Type => type;
        
        public Resource(ResourceType type)
        {
            this.type = type;
            amount = 0;
        }
            
        public Resource(ResourceType type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }

        public void Add(int value)
        {
            amount += value;
        }

        public void Remove(int value)
        {
            amount -= value;
        }
    }
}
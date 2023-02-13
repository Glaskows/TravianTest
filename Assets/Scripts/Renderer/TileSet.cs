using System;
using UnityEngine;

namespace Travian.Renderer
{
    [Serializable]
    public class RenderTile<T>
    {
        public T Type;
        public GameObject Prefab;
    }
    
    public class TileSet<T> : ScriptableObject
    {
        [Serializable]
        public class RenderTile : RenderTile<T>
        {}
        
        public RenderTile[] Tiles;
        [SerializeField] protected GameObject DefaultTile;

        public GameObject GetTilePrefab(T type)
        {
            foreach (var tile in Tiles)
            {
                if (tile.Type.Equals(type))
                    return tile.Prefab;
            }

            return DefaultTile;
        }
    }
}
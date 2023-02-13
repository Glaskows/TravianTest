using UnityEngine;

namespace Travian.Resource
{
    [CreateAssetMenu(fileName = "ResourcesConfig", menuName = "ScriptableObjects/Resources/ResourcesConfig")]
    public class ResourcesConfig : ScriptableObject
    {
        public Resource[] Resources;
    }
}
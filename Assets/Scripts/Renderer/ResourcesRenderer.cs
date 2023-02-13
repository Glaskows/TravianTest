using System;
using TMPro;
using UnityEngine;

namespace Travian.Renderer
{
    public class ResourcesRenderer : MonoBehaviour
    {
        [Serializable]
        public struct ResourceRenderer
        {
            public Resource.ResourceType Type;
            public TextMeshProUGUI Text;
        }

        [SerializeField] private ResourceRenderer[] resources;

        public void UpdateResourceValue(Resource.Resource resource)
        {
            int idx = Array.FindIndex(resources, r => r.Type == resource.Type);
            if (idx < 0)
                return;
            resources[idx].Text.SetText($"{resource.Amount}");
        }
    }
}
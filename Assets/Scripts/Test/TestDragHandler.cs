using UnityEngine;

namespace Travian.Test
{
    public class TestDragHandler : MonoBehaviour
    {
        public void LogHorizontal(float drag)
        {
            Debug.Log($"Horizontal drag: {drag}");
        }

        public void LogVertical(float drag)
        {
            Debug.Log($"Vertical drag: {drag}");
        }
    }
}
using JetBrains.Annotations;
using UnityEngine;

namespace Travian.Input
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float speedFactor;
        [SerializeField] private float damping;
        
        private Camera worldCamera;
        private Vector3 lookAt;
        private float rotationSpeed;
        private const float RotationThresshold = 0.01f;
        
        [UsedImplicitly]
        public void RotateAroundTarget(float displacement)
        {
            rotationSpeed = displacement * speedFactor;
        }
            
        private void Awake()
        {
            worldCamera = GetComponent<Camera>();
            lookAt = Vector3.zero;
        }

        private void Update()
        {
            if (Mathf.Abs(rotationSpeed) < RotationThresshold)
                return;
            
            rotationSpeed = Mathf.Lerp(rotationSpeed, 0f, damping);
            worldCamera.transform.RotateAround(lookAt, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
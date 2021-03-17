using UnityEngine;

namespace ProjectD.Camera
{
    public class FaceCamera : MonoBehaviour
    {
        private Transform mainCameraTransform;

        private void Start()
        {
            mainCameraTransform = UnityEngine.Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(
                transform.position + mainCameraTransform.rotation * Vector3.forward,
                mainCameraTransform.rotation * Vector3.up);
        }
    }
}
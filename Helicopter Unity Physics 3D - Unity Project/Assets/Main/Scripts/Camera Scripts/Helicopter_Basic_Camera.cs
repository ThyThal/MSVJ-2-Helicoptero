using UnityEngine;

namespace Helicopter_Camera
{
    public class Helicopter_Basic_Camera : Helicopter_Base_Camera, IHelicopter_Camera
    {
        #region • Variables (3)
        [Header("Basic Camera Properties")]
        public float height = 2f;
        public float distance = 2f;
        public float smoothSpeed = 0.35f;
        #endregion
        
        #region • Unity methods (2)
        void OnEnable()
        {
            updateEvent.AddListener(UpdateCamera);
        }

        void OnDisable()
        {
            updateEvent.RemoveListener(UpdateCamera);
        }
        #endregion

        #region • Custom interface methods (1)
        public void UpdateCamera()
        {
            //wanted position
            wantedPosition = rigidbody.position + (targetFlatFwd * distance) + (Vector3.up * height);

            //lets position the camera
            transform.position = Vector3.SmoothDamp(transform.position, wantedPosition, ref refVelocity, smoothSpeed);
            transform.LookAt(cameraTarget);
        }
        #endregion
    }
}
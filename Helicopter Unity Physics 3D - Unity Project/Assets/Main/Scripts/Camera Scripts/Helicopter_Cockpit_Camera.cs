using UnityEngine;

namespace Helicopter_Camera
{
    public class Helicopter_Cockpit_Camera : Helicopter_Base_Camera, IHelicopter_Camera
    {
        #region • Variables (3)
        [Header("Cockpit Camera Components")]
        public Transform cockpitPosition;
        
        [Header("Cockpit Camera Properties")]
        public Vector3 offset = Vector3.zero;
        public float fieldOfView = 70f;
        #endregion
        
        #region • Unity methods (Built-In) (2)
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
            if (cockpitPosition)
            {
                transform.position = cockpitPosition.position;
                transform.LookAt(cameraTarget);
            }
        }
        #endregion
    }
}
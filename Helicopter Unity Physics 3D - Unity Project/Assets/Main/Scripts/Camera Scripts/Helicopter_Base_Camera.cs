using UnityEngine;
using UnityEngine.Events;

namespace Helicopter_Camera
{
    public class Helicopter_Base_Camera : MonoBehaviour
    {
        #region • Variables
        [Header("Base Camera Components")]
        public Transform cameraTarget;
        public Rigidbody rigidbody;
        
        protected Vector3 wantedPosition;
        protected Vector3 refVelocity;
        protected UnityEvent updateEvent = new UnityEvent();
        #endregion
        
        #region • Properties (1)
        protected Vector3 targetFlatFwd;
        public Vector3 TargetFlatFwd => targetFlatFwd;
        #endregion
        
        #region • Unity methods (Built-In) (1)
        private void FixedUpdate()
        {
            if (rigidbody)
            {
                targetFlatFwd = rigidbody.transform.forward;
                targetFlatFwd.y = 0f;
                targetFlatFwd = targetFlatFwd.normalized;

                updateEvent.Invoke();
            }
        }
        #endregion
    }
}
using UnityEngine;

namespace Helicopter_Camera
{
    public class Helicopter_TopDown_Camera : Helicopter_Base_Camera, IHelicopter_Camera
    {
        #region • Variables
        [Header("Top Down Camera Properties")]
        public float height = 2f;
        public float distance = 2f;
        public float leadDistance = 0.25f;

        public float smoothTime = 0.15f;

        private Vector3 finalPosition;
        private Vector3 finalLead;
        private Vector3 refLeadVelocity;
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
            Vector3 targetPosition = rigidbody.position;
            targetPosition.y = 0f;

            wantedPosition = (Vector3.back * -distance) + (Vector3.up * height);

            Vector3 lead = rigidbody.velocity;
            lead.y = 0f;

            finalPosition = Vector3.SmoothDamp(finalPosition, targetPosition + wantedPosition, ref refVelocity, smoothTime);
            transform.position = finalPosition;

            finalLead = Vector3.SmoothDamp(finalLead, (lead * leadDistance), ref refLeadVelocity, smoothTime);
            transform.LookAt(cameraTarget.position + finalLead);
        }
        #endregion
    }
}
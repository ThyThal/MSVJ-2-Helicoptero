using UnityEngine;

namespace Helicopter_Camera
{
    public class Helicopter_Advanced_Camera : Helicopter_Base_Camera, IHelicopter_Camera
    {
        #region • Variables ()
        [Header("Height")]
        [Header("Advanced Camera Properties")]
        public float height = 2f;
        public float minGroundHeight = 4f;
        
        [Header("Distance")]
        public float minDistance = 4f;
        public float maxDistance = 8f;
        
        [Header("Speed")]
        public float catchUpModifier = 5f;
        public float rotationSpeed = 5f;
        public float minVelocityForOrient = 5f;

        private float finalAngle;
        private Vector3 wantedDir;
        private float finalHeight;
        #endregion
        
        #region • Unity methods (Built-In) (2)
        void OnEnable()
        {
            updateEvent.AddListener(UpdateCamera);
        }

        private void OnDisable()
        {
            updateEvent.RemoveListener(UpdateCamera);
        }
        #endregion
        
        #region • Custom interface methods (1)
        public void UpdateCamera()
        {
            Vector3 rigidbodyPosition = rigidbody.position;
            Vector3 directionToTarget = transform.position - rigidbodyPosition;
            directionToTarget.y = 0f;
            Vector3 normalizedDir = directionToTarget.normalized;
            wantedDir = normalizedDir;
            Debug.DrawRay(rigidbodyPosition, wantedDir, Color.green);
            
            float angleToFwd = Vector3.SignedAngle(normalizedDir, targetFlatFwd, Vector3.up);
            float wantedAngle = 0f;
            
            if (rigidbody.velocity.magnitude > minVelocityForOrient)
            {
                wantedAngle = angleToFwd * Time.fixedDeltaTime;
            }
            
            finalAngle = Mathf.Lerp(finalAngle, wantedAngle, Time.fixedDeltaTime * rotationSpeed);
            wantedDir  = Quaternion.AngleAxis(finalAngle, Vector3.up) * wantedDir;
            
            wantedPosition = rigidbodyPosition + (wantedDir * directionToTarget.magnitude);
            float curMagnitude = directionToTarget.magnitude;
            
            if (curMagnitude < minDistance)
            {
                float delta = minDistance - curMagnitude;
                wantedPosition += wantedDir * delta * Time.fixedDeltaTime * catchUpModifier;
            }
            
            if (curMagnitude > maxDistance)
            {
                float delta = curMagnitude - maxDistance;
                wantedPosition -= wantedDir * delta * Time.fixedDeltaTime * catchUpModifier;
            }
            
            float wantedheight = height;
            RaycastHit hit;
            Ray groundRay = new Ray(transform.position, Vector3.down);
            
            if(Physics.Raycast(groundRay, out hit, 100f))
            {
                if (hit.transform.tag == "ground" && hit.distance <= minGroundHeight)
                {
                    wantedheight += minGroundHeight - hit.distance;
                }
            }
            finalHeight = Mathf.Lerp(finalHeight, wantedheight, Time.fixedDeltaTime);
            
            transform.position = wantedPosition + (Vector3.up * finalHeight);
            transform.LookAt(cameraTarget);
        }
        #endregion
    }
}
using Helicopter_Components;
using Helicopter_Inputs;
using UnityEngine;

namespace Helicopter_Characteristics
{
    public class Helicopter_Simulator_Characteristics : MonoBehaviour
    {
        #region • Variables (10)
        [Header("Lift Properties")]
        public float maxLiftForce = 100f;
        public Helicopter_MainRotor mainRotor;
        [Space]

        [Header("Tail Rotor Properties")]
        public float tailForce = 2f;
        [Space]

        [Header("Cyclic Properties")]
        public float cyclicForce = 2f;
        public float cyclicForceMultiplier = 1000f;

        [Header("Auto Level Properties")]
        public float autoLevelForce = 2f;
        
        protected Vector3 flatForward;
        protected float forwardDot;
        protected Vector3 flatRight;
        protected float rightDot;
        #endregion
        
        #region • Custom methods (6)
        public void UpdateCharacteristics(Rigidbody rigidbody, Helicopter_Main_Input_Manager mainInputManager)
        {
            HandleCyclic(rigidbody, mainInputManager);
            HandleLift(rigidbody, mainInputManager);
            HandlePedals(rigidbody, mainInputManager);
            
            CalculateAngles();
            AutoLevel(rigidbody);
        }
        
        protected virtual void HandleCyclic(Rigidbody rigidbody, Helicopter_Main_Input_Manager mainInputManager)
        {
            
            float cyclicZForce = mainInputManager.CyclicInput.x * cyclicForce;
            rigidbody.AddRelativeTorque(Vector3.forward * cyclicZForce, ForceMode.Acceleration);

            float cyclicXForce = -mainInputManager.CyclicInput.y * cyclicForce;
            rigidbody.AddRelativeTorque(Vector3.right * cyclicXForce, ForceMode.Acceleration);
            
            // Apply force based off of the Dot Product values
            Vector3 forwardVec = flatForward * forwardDot;
            Vector3 rightVec = flatRight * rightDot;
            Vector3 finalCyclicDir = Vector3.ClampMagnitude(forwardVec + rightVec, 1f) * (cyclicForce * cyclicForceMultiplier);

            rigidbody.AddForce(finalCyclicDir, ForceMode.Force);
        }

        protected virtual void HandleLift(Rigidbody rigidbody, Helicopter_Main_Input_Manager mainInputManager)
        {
            if (mainRotor)
            {
                Vector3 liftForce = transform.up * ((Physics.gravity.magnitude + maxLiftForce) * rigidbody.mass);
                
                float normalizedRPMs = mainRotor.CurrentRPMs / 500f;
                
                float poweredCollective = Mathf.Pow(mainInputManager.StickyCollectiveInput, 2f);
                float poweredRPMs = Mathf.Pow(normalizedRPMs, 2f);
                
                rigidbody.AddForce(liftForce * (poweredRPMs * poweredCollective), ForceMode.Force);
            }
        }
        
        protected virtual void HandlePedals(Rigidbody rigidbody, Helicopter_Main_Input_Manager mainInputManager)
        {
            rigidbody.AddTorque(Vector3.up * (mainInputManager.PedalInput * tailForce), ForceMode.Acceleration);
        }

        private void CalculateAngles()
        {
            Vector3 transformPosition = transform.position;
            
            // Calcualte flat forward
            flatForward = transform.forward;
            flatForward.y = 0f;
            flatForward = flatForward.normalized;
            Debug.DrawRay(transformPosition, flatForward, Color.blue);

            // Calculate flat right
            flatRight = transform.right;
            flatRight.y = 0f;
            flatRight = flatRight.normalized;
            Debug.DrawRay(transformPosition, flatRight, Color.red);

            // Calculate angles
            forwardDot = Vector3.Dot(transform.up, flatForward);
            rightDot = Vector3.Dot(transform.up, flatRight);
            
        }

        protected virtual void AutoLevel(Rigidbody rigidbody)
        {
            float rightForce = -forwardDot * autoLevelForce;
            float forwardForce = rightDot * autoLevelForce;

            rigidbody.AddRelativeTorque(Vector3.right * rightForce, ForceMode.Acceleration);
            rigidbody.AddRelativeTorque(Vector3.forward * forwardForce, ForceMode.Acceleration);
        }
        #endregion
    }
}
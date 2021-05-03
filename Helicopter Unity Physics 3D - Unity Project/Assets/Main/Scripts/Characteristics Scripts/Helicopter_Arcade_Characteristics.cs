using Helicopter_Inputs;
using UnityEngine;

namespace Helicopter_Characteristics
{
    public class Helicopter_Arcade_Characteristics : Helicopter_Simulator_Characteristics
    {
        #region • Variables (6)
        [Header("Arcade Properties")]
        public float bankAngle = 35f;
        public float bankSpeed = 4f;

        private float rotationX = 0f;
        private float rotationY = 0f;
        private float rotationZ = 0f;

        Quaternion finalRot = Quaternion.identity;
        #endregion

        #region • Custom methods (4)
        protected override void HandleCyclic(Rigidbody rigidbody, Helicopter_Main_Input_Manager mainInputManager)
        {
            //base.HandleCyclic(rb, input);
            Vector3 fwdDir = mainInputManager.CyclicInput.y * flatForward;
            Vector3 rightDir = mainInputManager.CyclicInput.x * flatRight;
            Vector3 finalDir = (fwdDir + rightDir).normalized;

            rigidbody.AddForce(finalDir * cyclicForce, ForceMode.Acceleration);

            rotationX = mainInputManager.CyclicInput.y * bankAngle;
            rotationZ = -mainInputManager.CyclicInput.x * bankAngle;
        }
        
        protected override void HandleLift(Rigidbody rigidbody, Helicopter_Main_Input_Manager mainInputManager)
        {
            //base.HandleLift(rb, input);
            Vector3 liftForce = Vector3.up * (Physics.gravity.magnitude * rigidbody.mass);
            rigidbody.AddForce(liftForce, ForceMode.Force);

            rigidbody.AddForce(Vector3.up * (mainInputManager.ThrottleInput * maxLiftForce), ForceMode.Acceleration);
        }
        
        protected override void HandlePedals(Rigidbody rigidbody, Helicopter_Main_Input_Manager mainInputManager)
        {
            //base.HandlePedals(rb, input);
            rotationY += mainInputManager.PedalInput * tailForce;
        }

        protected override void AutoLevel(Rigidbody rigidbody)
        {
            Quaternion wantedRot = Quaternion.Euler(rotationX, rotationY, rotationZ);
            finalRot = Quaternion.Slerp(finalRot, wantedRot, Time.fixedDeltaTime * bankSpeed);
            rigidbody.MoveRotation(finalRot);
        }
        #endregion
    }
}
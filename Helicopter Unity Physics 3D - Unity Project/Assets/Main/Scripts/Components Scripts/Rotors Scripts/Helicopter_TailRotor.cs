using Helicopter_Inputs;
using UnityEngine;

namespace Helicopter_Components
{
    public class Helicopter_TailRotor : MonoBehaviour, IHelicopter_Rotor
    {
        #region • Variables (4)
        [Header("Tail Rotor Components")]
        public Transform leftBlade;
        public Transform rightBlade;
        
        [Header("Tail Rotor Properties")]
        public float maxPitch = 45f;
        public float rotationSpeedModifier = 1.5f;
        #endregion
        
        #region • Custom interface methods (1)
        public void UpdateRotor(float dps, Helicopter_Main_Input_Manager mainInputManager)
        {
            //Debug.Log("Updating Tail Rotor");
            transform.Rotate(Vector3.right, dps * rotationSpeedModifier * Time.deltaTime);

            //Pitch the blased up and down
            if (leftBlade && rightBlade)
            {
                leftBlade.localRotation = Quaternion.Euler(0f, mainInputManager.PedalInput * maxPitch, 0f);
                rightBlade.localRotation = Quaternion.Euler(0f, -mainInputManager.PedalInput * maxPitch, 0f);
            }
        }
        #endregion
    }
}
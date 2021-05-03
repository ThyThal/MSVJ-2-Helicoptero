using Helicopter_Inputs;
using UnityEngine;

namespace Helicopter_Components
{
    public class Helicopter_MainRotor : MonoBehaviour, IHelicopter_Rotor
    {
        #region • Variables (5)
        [Header("Main Rotor Components")]
        public Transform leftBlade;
        public Transform rightBlade;
        
        [Header("Main Rotor Properties")]
        public float maxPitch = 35f;
        
        [HideInInspector]
        public Vector2 cyclicVal;
        #endregion
        
        #region • Properties (1)
        private float currentRPMs;
        public float CurrentRPMs => currentRPMs;
        #endregion
        
        #region • Custom interface methods (1)
        public void UpdateRotor(float dps, Helicopter_Main_Input_Manager mainInputManager)
        {
            currentRPMs = (dps / 360) * 60f;
            
            transform.Rotate(Vector3.up, dps * Time.deltaTime * 0.5f);
            
            Vector3 discNormal = Vector3.Normalize(transform.up + new Vector3(-cyclicVal.x, 0f, -cyclicVal.y));
            
            if (leftBlade && rightBlade)
            {
                cyclicVal = mainInputManager.CyclicInput;

                leftBlade.localRotation = Quaternion.Euler(-mainInputManager.StickyCollectiveInput * maxPitch, 0f, 0f);
                rightBlade.localRotation = Quaternion.Euler(mainInputManager.StickyCollectiveInput * maxPitch, 0f, 0f);
            }
        }
        #endregion
    }
}
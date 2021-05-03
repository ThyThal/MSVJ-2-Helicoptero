using UnityEngine;

namespace Helicopter_Components
{
    public class Helicopter_Engine : MonoBehaviour
    {
        #region • Variables (4)
        [Header("Engine Properties")]
        public float maxHP  = 140f;
        public float maxRPM = 2700f;
        public float powerDelay = 2f;
        
        [Header("Engine Graph")]
        public AnimationCurve powerCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
        #endregion
        
        #region • Properties (2)
        private float currentHP;
        public float CurrentHP => currentHP;

        private float currentRPM;
        public float CurrentRPM => currentRPM;
        #endregion
        
        #region • Custom methods (1)
        public void UpdateEngine(float throttleInput)
        {
            //Calculate Horsepower
            float wantedHP = powerCurve.Evaluate(throttleInput) * maxHP;
            currentHP = Mathf.Lerp(currentHP, wantedHP, Time.deltaTime * powerDelay);

            // 
            float wantedRPM = throttleInput * maxRPM;
            currentRPM = Mathf.Lerp(currentRPM, wantedRPM, Time.deltaTime * powerDelay);
        }
        #endregion
    }
}
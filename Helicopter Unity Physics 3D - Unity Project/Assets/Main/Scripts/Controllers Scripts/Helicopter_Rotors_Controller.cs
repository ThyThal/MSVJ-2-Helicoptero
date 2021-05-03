using Helicopter_Inputs;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Helicopter_Components
{
    public class Helicopter_Rotors_Controller : MonoBehaviour
    {
        #region • Variables (3)
        [Header("Rotors Properties")]
        public bool isArcade = false;
        public float maxDps  = 3000f;
        private List<IHelicopter_Rotor> rotors;
        #endregion

        #region • Unity methods (Built-In) (1)
        private void Start()
        {
            rotors = GetComponentsInChildren<IHelicopter_Rotor>().ToList<IHelicopter_Rotor>();
        }
        #endregion

        #region • Custom methods (1)
        public void UpdateRotors(Helicopter_Main_Input_Manager input, float curentRPMs)
        {
            float dps = ((curentRPMs * 360f) / 60f);
            dps = Mathf.Clamp(dps, 0f, maxDps);

            if (isArcade)
            {
                dps = 4000f;
            }
            
            if (rotors.Count > 0)
            {
                foreach(var rotor in rotors)
                {
                    rotor.UpdateRotor(dps, input);
                }
            }
        }
        #endregion
    }
}
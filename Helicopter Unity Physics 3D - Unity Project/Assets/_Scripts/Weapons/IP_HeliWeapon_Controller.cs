using Helicopter_Inputs;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Menu_Editor
{
    public class IP_HeliWeapon_Controller : MonoBehaviour
    {
        #region Variables
        [Header("Wepaon Controller Properties")]
        public bool allowFiring = true;

        private List<IP_IWeapon> weapons = new List<IP_IWeapon>();
        #endregion


        #region Main Methods
        // Start is called before the first frame update
        void Start()
        {
            weapons = GetComponentsInChildren<IP_IWeapon>().ToList<IP_IWeapon>();
        }
        #endregion


        #region Custom Methods
        public void UpdateWeapons(Helicopter_Main_Input_Manager input)
        {
            if (input.Fire)
            {
                if (weapons.Count > 0 && allowFiring)
                {
                    foreach (IP_IWeapon weapon in weapons)
                    {
                        weapon.FireWeapon();
                    }
                }
            }
        }
        #endregion

    }
}

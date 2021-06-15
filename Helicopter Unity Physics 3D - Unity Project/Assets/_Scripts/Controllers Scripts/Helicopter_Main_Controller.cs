using Helicopter_Components;
using Helicopter_Inputs;
using System.Collections.Generic;
using Helicopter_Characteristics;
using UnityEngine;

namespace Helicopter_Controllers
{
    [RequireComponent(typeof(Helicopter_Main_Input_Manager), typeof(Helicopter_Simulator_Characteristics))]
    public class Helicopter_Main_Controller : Base_Rigidbody_Controller
    {
        #region • Variables (5)
        [Header("Helicopter Engines")]
        public List<Helicopter_Engine> engines = new List<Helicopter_Engine>();

        [Header("Helicopter Rotors")]
        public Helicopter_Rotors_Controller rotorsController;

        private Helicopter_Main_Input_Manager mainInputManager;
        private Helicopter_Simulator_Characteristics simulatorCharacteristics;
        #endregion

        #region • Unity methods (Built-In) (1)
        public override void Start()
        {
            base.Start();
            
            // Get components on Start
            mainInputManager = GetComponent<Helicopter_Main_Input_Manager>();
            simulatorCharacteristics = GetComponent<Helicopter_Simulator_Characteristics>();
        }
        #endregion
        
        #region • Custom methods (1)
        protected override void HandlePhysics()
        {
            if (mainInputManager)
            {
                HandleCharacteristics();
                HandleEngines();
                HandleRotors();
            }
        }
        #endregion
        
        #region • Helicopter control methods (4)
        protected virtual void HandleCharacteristics()
        {
            if (simulatorCharacteristics)
            {
                simulatorCharacteristics.UpdateCharacteristics(rigidbody, mainInputManager);
            }
        }
        
        protected virtual void HandleEngines()
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i].UpdateEngine(mainInputManager.StickyThrottle);
            }
        }

        protected virtual void HandleRotors()
        {
            if (rotorsController && engines.Count > 0)
            {
                rotorsController.UpdateRotors(mainInputManager, engines[0].CurrentRPM);
            }
        }
        #endregion
        
    }
}
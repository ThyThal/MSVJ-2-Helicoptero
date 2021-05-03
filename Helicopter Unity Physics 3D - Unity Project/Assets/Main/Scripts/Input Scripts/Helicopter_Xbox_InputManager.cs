using UnityEngine;

namespace Helicopter_Inputs
{
    public class Helicopter_Xbox_InputManager : Helicopter_Keyboard_InputManager
    {
        #region • Custom methods (7)
        protected override void HandleCameraButton()
        {
            cameraInput = Input.GetButtonDown("XBox Camera Button");
        }
        
        protected override void HandleCollective()
        {
            collectiveInput = Input.GetAxis("XBox Collective");
        }
        
        protected override void HandleCyclic()
        {
            cyclicInput.y = Input.GetAxis("XBox Cyclic Vertical");
            cyclicInput.x = Input.GetAxis("XBox Cyclic Horizontal");
        }
        
        protected override void HandlePedal()
        {
            pedalInput = Input.GetAxis("XBox Pedal");
        }
        
        protected override void HandleThrottle()
        {
            throttleInput = Input.GetAxis("XBox Throttle Up") + -Input.GetAxis("XBox Throttle Down");
        }
        #endregion
    }
}
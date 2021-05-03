using UnityEngine;

namespace Helicopter_Inputs
{
    public class Helicopter_Keyboard_InputManager : Helicopter_Base_InputManager
    {
        #region • Variables (2)
        [Header("Camera Input Properties")]
        public KeyCode cameraButton = KeyCode.C;
        #endregion

        #region • Properties (8)
        protected bool cameraInput = false;
        public bool CameraInput => cameraInput;
        
        protected float collectiveInput = 0f;
        public float CollectiveInput => collectiveInput;
        
        protected Vector2 cyclicInput = Vector2.zero;
        public Vector2 CyclicInput => cyclicInput;
        
        protected bool fire = false;
        public bool Fire => fire;
        
        protected float pedalInput = 0f;
        public float PedalInput => pedalInput;
        
        protected float throttleInput = 0f;
        public float RawThrottleInput => throttleInput;
        
        protected float stickyCollectiveInput = 0f;
        public float StickyCollectiveInput => stickyCollectiveInput;

        protected float stickyThrottle = 0f;
        public float StickyThrottle => stickyThrottle;
        #endregion
        
        #region • Custom methods (10)
        protected override void HandleInputs()
        {
            base.HandleInputs();

            //Input methods
            HandleCameraButton();
            HandleCollective();
            HandleCyclic();
            HandlePedal();
            HandleThrottle();
            
            //Utility methods
            ClampInputs();
            HandleStickyCollective();
            HandleStickyThrottle();
        }

        #region • Input methods (6)
        protected virtual void HandleCameraButton()
        {
            cameraInput = Input.GetKeyDown(cameraButton);
        }
        
        protected virtual void HandleCollective()
        {
            collectiveInput = Input.GetAxis("Collective");
        }

        protected virtual void HandleCyclic()
        {
            cyclicInput.y = vertical;
            cyclicInput.x = horizontal; 
        }
        
        protected virtual void HandlePedal()
        {
            pedalInput = Input.GetAxis("Pedal");
        }
        
        protected virtual void HandleThrottle()
        {
            throttleInput = Input.GetAxis("Throttle");
        }
        #endregion

        #region • Utility methods (3)
        protected void ClampInputs()
        {
            collectiveInput = Mathf.Clamp(collectiveInput, -1f, 1f);
            cyclicInput = Vector2.ClampMagnitude(cyclicInput, 1);
            pedalInput = Mathf.Clamp(pedalInput, -1f, 1f);
            throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);
        }
        
        protected void HandleStickyCollective()
        {
            stickyCollectiveInput += -collectiveInput * Time.deltaTime;
            stickyCollectiveInput = Mathf.Clamp01(stickyCollectiveInput);
        }

        protected void HandleStickyThrottle()
        {
            stickyThrottle += RawThrottleInput * Time.deltaTime;
            stickyThrottle = Mathf.Clamp01(stickyThrottle);
        }
        #endregion
        #endregion
    }
}
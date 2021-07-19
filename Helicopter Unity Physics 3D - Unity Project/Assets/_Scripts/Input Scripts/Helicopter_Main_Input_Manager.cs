using UnityEngine;
using UnityEngine.Events;

namespace Helicopter_Inputs
{
    public enum InputType
    {
        Keyboard,
        Xbox,
    }

    [RequireComponent(typeof(Helicopter_Keyboard_InputManager))]
    public class Helicopter_Main_Input_Manager : MonoBehaviour
    {
        #region • Variables (4)
        [Header("Input Properties")]
        public InputType inputType = InputType.Keyboard;

        [Header("Input Events")]
        public UnityEvent onCameraButtonPressed = new UnityEvent();

        private Helicopter_Keyboard_InputManager keyInputManager;
        //private Helicopter_Xbox_InputManager xboxInputManager;
        #endregion
        
        #region • Properties (8)
        private bool cameraInput;
        public bool CameraInput => cameraInput;
        
        private float collectiveInput;
        public float CollectiveInput => collectiveInput;
        
        private Vector2 cyclicInput;
        public Vector2 CyclicInput => cyclicInput;
        
        private bool fire;
        public bool Fire => fire;
        
        private float pedalInput;
        public float PedalInput => pedalInput;
        
        private float stickyCollectiveInput;
        public float StickyCollectiveInput => stickyCollectiveInput;
        
        private float stickyThrottle;
        public float StickyThrottle => stickyThrottle;
        
        private float throttleInput;
        public float ThrottleInput => throttleInput;
        #endregion
        
        #region • Unity methods (2)
        private void Start()
        {
            keyInputManager = GetComponent<Helicopter_Keyboard_InputManager>();
            //xboxInputManager = GetComponent<Helicopter_Xbox_InputManager>();

            if (keyInputManager)
            {
                SetInputType(inputType);
            }
        }

        private void Update()
        {
            if (keyInputManager)
            {
                switch (inputType)
                {
                    case InputType.Keyboard:
                        cameraInput = keyInputManager.CameraInput;
                        collectiveInput = keyInputManager.CollectiveInput;
                        cyclicInput = keyInputManager.CyclicInput;
                        fire = keyInputManager.Fire;
                        pedalInput = keyInputManager.PedalInput;
                        stickyCollectiveInput = keyInputManager.StickyCollectiveInput;
                        stickyThrottle = keyInputManager.StickyThrottle;
                        throttleInput = keyInputManager.RawThrottleInput;
                        break;

                    //case InputType.Xbox:
                    //    cameraInput = xboxInputManager.CameraInput;
                    //    collectiveInput = xboxInputManager.CollectiveInput;
                    //    cyclicInput = xboxInputManager.CyclicInput;
                    //    fire = xboxInputManager.Fire;
                    //    pedalInput = xboxInputManager.PedalInput;
                    //    stickyCollectiveInput = xboxInputManager.StickyCollectiveInput;
                    //    stickyThrottle = xboxInputManager.StickyThrottle;
                    //    throttleInput = xboxInputManager.RawThrottleInput;
                    //    break;

                    default:
                        break;
                }

                if (cameraInput)
                {
                    onCameraButtonPressed.Invoke();
                }
            }
        }
        #endregion
        
        #region • Custom methods (1)
        void SetInputType (InputType type)
        {
            if (type == InputType.Keyboard)
            {
                keyInputManager.enabled = true;
                ////xboxInputManager.enabled = false;
            }

            if (type == InputType.Xbox)
            {
                keyInputManager.enabled = false;
                //xboxInputManager.enabled = true;
            }
        }
        #endregion
    }
}
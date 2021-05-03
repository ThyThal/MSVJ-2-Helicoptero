using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Helicopter_Camera
{
    public class Helicopter_Camera_Manager : MonoBehaviour
    {
        #region • Variables (3)
        [Header("Manager Properties")]
        public int startIndex = 1;

        private List<Helicopter_Base_Camera> cameras = new List<Helicopter_Base_Camera>();
        private int cameraIndex = 0;
        #endregion
        
        #region • Unit methods (1)
        void Start()
        {
            cameras = transform.GetComponentsInChildren<Helicopter_Base_Camera>().ToList<Helicopter_Base_Camera>();
            cameraIndex = startIndex;
            
            SwitchCamera(cameraIndex);
        }
        #endregion
        
        #region • Custom methods (3)
        public void SwitchCamera()
        {
            cameraIndex++;
            HandleSwitch();
        }

        public void SwitchCamera(int index)
        {
            HandleSwitch();
        }

        private void HandleSwitch()
        {
            if (cameraIndex == cameras.Count)
            {
                cameraIndex = 0;
            }

            for (int camera = 0; camera < cameras.Count; camera++)
            {
                //cameras[i].gameObject.SetActive(false);
                Camera currentCamera = cameras[camera].GetComponent<Camera>();
                if (currentCamera)
                {
                    currentCamera.enabled = false;
                    
                    if (camera == cameraIndex)
                    {
                        //cameras[camIndex].gameObject.SetActive(true);
                        currentCamera.enabled = true;
                    }
                }
            }
        }
        #endregion
    }
}
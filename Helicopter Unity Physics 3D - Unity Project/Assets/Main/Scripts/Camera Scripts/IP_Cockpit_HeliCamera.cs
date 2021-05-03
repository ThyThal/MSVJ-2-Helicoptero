using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Menu_Editor
{
    public class IP_Cockpit_HeliCamera : IP_Base_HeliCamera, IP_IHeliCamera
    {
        #region Variables
        [Header("Cockpit Camera Properties")]
        public Transform cockpitPosition;
        public Vector3 offset = Vector3.zero;
        public float fov = 70f;
        #endregion


        #region builtin Methods
        void OnEnable()
        {
            updateEvent.AddListener(UpdateCamera);
        }

        void OnDisable()
        {
            updateEvent.RemoveListener(UpdateCamera);
        }
        #endregion


        #region Interface Methods
        public void UpdateCamera()
        {
            if(cockpitPosition)
            {
                transform.position = cockpitPosition.position;
                transform.LookAt(lookAtTarget);
            }
        }
        #endregion
    }
}

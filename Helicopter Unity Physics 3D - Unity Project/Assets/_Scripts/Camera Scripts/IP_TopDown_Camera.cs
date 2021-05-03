using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu_Editor
{
    public class IP_TopDown_Camera : IP_Base_HeliCamera, IP_IHeliCamera
    {
        #region Variables
        [Header("Top Down Camera Properties")]
        public float height = 2f;
        public float distance = 2f;
        public float leadDistance = 0.25f;

        public float smoothTime = 0.15f;

        private Vector3 finalPosition;
        private Vector3 finalLead;
        private Vector3 refLeadVelocity;
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
            //Debug.Log("Updating Top Down Camera!");
            Vector3 targetPos = rb.position;
            targetPos.y = 0f;

            wantedPos = (Vector3.back * -distance) + (Vector3.up * height);

            Vector3 lead = rb.velocity;
            lead.y = 0f;

            finalPosition = Vector3.SmoothDamp(finalPosition, targetPos + wantedPos, ref refVelocity, smoothTime);
            transform.position = finalPosition;

            finalLead = Vector3.SmoothDamp(finalLead, (lead * leadDistance), ref refLeadVelocity, smoothTime);
            transform.LookAt(lookAtTarget.position + finalLead);
        }
        #endregion
    }
}

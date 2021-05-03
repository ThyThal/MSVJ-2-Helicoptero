using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Menu_Editor
{
    public class IP_Base_HeliCamera : MonoBehaviour
    {
        #region Variables
        [Header("Base Camera Properties")]
        public Rigidbody rb;
        public Transform lookAtTarget;

        protected Vector3 wantedPos;
        protected Vector3 refVelocity;
        protected UnityEvent updateEvent = new UnityEvent();
        protected Vector3 targetFlatFwd;
        #endregion


        #region Propterties
        public Vector3 TargetFlatFwd
        {
            get { return targetFlatFwd; }
        }
        #endregion


        #region BultIn Methods
        // Start is called before the first frame update
        void Start()
        {

        }

        //Physics Update
        void FixedUpdate()
        {
            if (rb)
            {
                //Get Flat Forward of the Target
                targetFlatFwd = rb.transform.forward;
                targetFlatFwd.y = 0f;
                targetFlatFwd = targetFlatFwd.normalized;

                updateEvent.Invoke();
            }
        }
        #endregion
    }
}

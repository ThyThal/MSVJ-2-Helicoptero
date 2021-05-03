using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Helicopter_Camera
{
    [CustomEditor(typeof(Helicopter_Advanced_Camera))]
    public class IP_AdvancedHeliCamera_Editor : Editor
    {
        #region Variables
        Helicopter_Advanced_Camera targetCamera;
        #endregion

        #region Methods
        private void OnEnable()
        {
            targetCamera = (Helicopter_Advanced_Camera)target;
        }

        private void OnSceneGUI()
        {
            float minDist = targetCamera.minDistance;
            float maxDist = targetCamera.maxDistance;

            if (targetCamera.rigidbody)
            {
                Vector3 targetFwd = targetCamera.rigidbody.transform.forward;
                targetFwd.y = 0f;
                targetFwd = targetFwd.normalized;

                Handles.color = Color.blue;
                Handles.DrawWireDisc(targetCamera.rigidbody.position, Vector3.up, minDist);
                Handles.DrawWireDisc(targetCamera.rigidbody.position, Vector3.up, maxDist);

                targetCamera.minDistance = Handles.ScaleSlider(targetCamera.minDistance, targetCamera.rigidbody.position + (targetFwd * minDist), Vector3.forward, Quaternion.identity, 1f, 1f);
                targetCamera.maxDistance = Handles.ScaleSlider(targetCamera.maxDistance, targetCamera.rigidbody.position + (targetFwd * maxDist), Vector3.forward, Quaternion.identity, 1f, 1f);
            }
        }
        #endregion
    }
}
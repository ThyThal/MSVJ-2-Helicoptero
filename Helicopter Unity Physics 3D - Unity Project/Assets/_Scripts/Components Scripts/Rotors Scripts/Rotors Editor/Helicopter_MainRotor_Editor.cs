using Helicopter_Components;
using UnityEditor;

namespace Helicopter_Rotors_Editor
{
    [CustomEditor(typeof(Helicopter_MainRotor))]
    public class Helicopter_MainRotor_Editor : Editor
    {
        #region • Variables (1)
        private Helicopter_MainRotor targetRotor;
        #endregion

        #region • Unity methods (Built-In) (3)
        private void OnEnable()
        {
            targetRotor = (Helicopter_MainRotor)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Repaint();
        }

        private void OnSceneGUI()
        {
            //Vector3 discNormal = Vector3.Normalize(targetRotor.transform.up + new Vector3(-targetRotor.cyclicVal.x, 0f, -targetRotor.cyclicVal.y) * 0.1f);

            //Handles.color = new Color(0f, 1f, 0f, 0.15f);
            //Handles.DrawSolidDisc(targetRotor.transform.position + (targetRotor.transform.up * 0.1f), discNormal, targetRotor.radius);

            //Handles.color = Color.green;
            //Handles.DrawWireDisc(targetRotor.transform.position + (targetRotor.transform.up * 0.1f), discNormal, targetRotor.radius);

            //Handles.color = Color.red;
            //Handles.ArrowHandleCap(0, targetRotor.lRotor.position, Quaternion.LookRotation(-targetRotor.transform.right), 1f, EventType.Repaint);
            //Handles.ArrowHandleCap(0, targetRotor.rRotor.position, Quaternion.LookRotation(targetRotor.transform.right), 1f, EventType.Repaint);

            //Repaint();
        }
        #endregion
    }
}
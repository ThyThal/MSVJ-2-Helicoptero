using UnityEngine;
using UnityEditor;

namespace Helicopter_Components
{
    [CustomEditor(typeof(Helicopter_Engine))]
    public class Helicopter_Engine_Editor : Editor
    {
        #region • Variables (1)
        private Helicopter_Engine targetEngine;
        #endregion

        #region • Unity methods (Built-In) (2)
        private void OnEnable()
        {
            targetEngine = (Helicopter_Engine)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Engine Stats:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("RPM's: " + targetEngine.CurrentRPM);
            EditorGUILayout.LabelField("HP: " + targetEngine.CurrentHP);

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();

            Repaint();
        }
        #endregion
    }
}
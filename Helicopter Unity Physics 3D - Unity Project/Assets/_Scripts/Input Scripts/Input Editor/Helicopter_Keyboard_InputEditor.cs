using Helicopter_Inputs;
using UnityEditor;

namespace Helicopter_Inputs_Editor
{
    [CustomEditor(typeof(Helicopter_Keyboard_InputManager))]
    public class Helicopter_Keyboard_InputEditor : Editor
    {
        #region • Variables (1)
        Helicopter_Keyboard_InputManager targetInputManager;
        #endregion
        
        #region • Unity methods (Built-In) (2)
        private void OnEnable()
        {
            targetInputManager = (Helicopter_Keyboard_InputManager)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DrawDebugUI();

            Repaint();
        }
        #endregion
        
        #region • Custom methods (1)
        void DrawDebugUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();

            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Throttle: " + targetInputManager.RawThrottleInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Collective: " + targetInputManager.CollectiveInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Cyclic: " + targetInputManager.CyclicInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Pedal: " + targetInputManager.PedalInput.ToString("0.00"), EditorStyles.boldLabel);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }
        #endregion
    }
}

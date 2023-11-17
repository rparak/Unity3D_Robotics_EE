using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
    // Create a custom property drawer for the [ReadOnly] attribute.
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;

            EditorGUI.PropertyField(position, property, label, true);

            GUI.enabled = true;
        }
    }

    // Create a custom attribute to use in the script.
    public class ReadOnlyAttribute : PropertyAttribute { }
#endif

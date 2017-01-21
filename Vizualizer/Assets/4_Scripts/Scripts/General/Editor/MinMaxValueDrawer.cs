using UnityEditor;
using UnityEngine;

// IngredientDrawer
[CustomPropertyDrawer (typeof (MinMaxValue))]
public class MinMaxValueDrawer : PropertyDrawer {

    // Draw the property inside the given rect
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty (position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        int padding = 2;
        float width = (position.width-padding) /2;
        // Calculate rects
        var minRect = new Rect (position.x, position.y, width, position.height);
        var maxRect = new Rect (position.x+ minRect.width+padding, position.y, width, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField (minRect, property.FindPropertyRelative ("_min"), GUIContent.none);
        EditorGUI.PropertyField (maxRect, property.FindPropertyRelative ("_max"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty ();
    }
}

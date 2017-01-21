using UnityEditor;
using UnityEngine;

// IngredientDrawer
[CustomPropertyDrawer (typeof (CurveAnimatedValue))]
public class CurveAnimatedValueDrawer : PropertyDrawer {

    // Draw the property inside the given rect
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty (position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        int durationWidth = 40;
        int padding = 2;
        // Calculate rects
        var curveRect = new Rect (position.x, position.y, position.width-durationWidth, position.height);
        var durationRect = new Rect (position.x+ curveRect.width+padding, position.y, durationWidth, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField (curveRect, property.FindPropertyRelative ("_curve"), GUIContent.none);
        EditorGUI.PropertyField (durationRect, property.FindPropertyRelative ("_duration"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty ();
    }
}

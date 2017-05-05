using UnityEditor;
using UnityEngine;
using ImageStorm;

// IngredientDrawer
[CustomPropertyDrawer (typeof (ImageStormImage))]
public class ImageStormImageDrawer : PropertyDrawer 
{
	// Draw the property inside the given rect
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty (position, label, property);

		// Draw label
		position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		int durationWidth = 60;
		int rotationWidth = 60;
		int curveWidth = 40;
		int padding = 2;
		// Calculate rects
		var curveRect = new Rect (position.x, position.y, curveWidth, position.height);
		var durationRect = new Rect (position.x+ curveRect.width+padding, position.y, durationWidth, position.height);
		var rotationRect = new Rect (durationRect.position.x+ durationRect.width+padding, position.y, rotationWidth, position.height);
		var textureRect = new Rect (rotationRect.position.x+ durationRect.width+padding, position.y, position.width-durationWidth -curveWidth -rotationWidth, position.height);

		// Draw fields - passs GUIContent.none to each so they are drawn without labels
		EditorGUI.PropertyField (curveRect, property.FindPropertyRelative ("_curve"), GUIContent.none);
		EditorGUI.PropertyField (durationRect, property.FindPropertyRelative ("_appearDuration"), GUIContent.none);
		EditorGUI.PropertyField (textureRect, property.FindPropertyRelative ("_texture"), GUIContent.none);
		EditorGUI.PropertyField (rotationRect, property.FindPropertyRelative ("_randomRotation"), GUIContent.none);

		// Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty ();
	}
}

[CustomPropertyDrawer (typeof (ImageStormSimpleImage))]
public class ImageStormSimpleImageDrawer : PropertyDrawer 
{
	// Draw the property inside the given rect
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty (position, label, property);

		// Draw label
		position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		int durationWidth = 60;
		int rotationWidth = 60;
		int curveWidth = 40;
		int padding = 2;
		// Calculate rects
		var curveRect = new Rect (position.x, position.y, curveWidth, position.height);
		var durationRect = new Rect (position.x+ curveRect.width+padding, position.y, durationWidth, position.height);
		var rotationRect = new Rect (durationRect.position.x+ durationRect.width+padding, position.y, rotationWidth, position.height);
		var textureRect = new Rect (rotationRect.position.x+ durationRect.width+padding, position.y, position.width-durationWidth -curveWidth -rotationWidth, position.height);

		// Draw fields - passs GUIContent.none to each so they are drawn without labels
		EditorGUI.PropertyField (curveRect, property.FindPropertyRelative ("_curve"), GUIContent.none);
		EditorGUI.PropertyField (durationRect, property.FindPropertyRelative ("_appearDuration"), GUIContent.none);
		EditorGUI.PropertyField (textureRect, property.FindPropertyRelative ("_texture"), GUIContent.none);
		EditorGUI.PropertyField (rotationRect, property.FindPropertyRelative ("_randomRotation"), GUIContent.none);

		// Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty ();
	}
}
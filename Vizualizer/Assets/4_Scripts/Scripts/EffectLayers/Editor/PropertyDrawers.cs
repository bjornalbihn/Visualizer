using UnityEditor;
using UnityEngine;

/*
[CustomPropertyDrawer (typeof (ShaderFloat))]
public class ShaderFloatDrawer : PropertyDrawer
{
	const float lineHeight = 16;

	public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
	{
		SerializedProperty name = prop.FindPropertyRelative("_property");
		SerializedProperty values = prop.FindPropertyRelative("_values");

		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		//EditorGUI.PropertyField ( new Rect (pos.x, pos.y, pos.width/3, lineHeight), name, GUIContent.none);
		EditorGUI.PropertyField ( new Rect (pos.x+pos.width/3, lineHeight, pos.width/3*2, pos.height), values, GUIContent.none);
		EditorGUI.indentLevel = indent;
	}
}
*/
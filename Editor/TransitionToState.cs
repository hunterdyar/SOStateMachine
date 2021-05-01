using UnityEditor;
using UnityEngine;

namespace Bloops.StateMachine.Editor
{
	[CustomPropertyDrawer(typeof(StateMachine.TransitionToState))]
	public class TransitionToState : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			// Using BeginProperty / EndProperty on the parent property means that
			// prefab override logic works on the entire property.
			EditorGUI.BeginProperty(position, label, property);

			// Draw label
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			// Don't make child fields be indented
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			// Calculate rects
			var transitionRect = new Rect(position.x, position.y, 80, position.height);
			var stateRect = new Rect(position.x + 100, position.y, position.width - 100, position.height);

			// Draw fields - passs GUIContent.none to each so they are drawn without labels
			EditorGUI.PropertyField(transitionRect, property.FindPropertyRelative("transition"), GUIContent.none);
			EditorGUI.PropertyField(stateRect, property.FindPropertyRelative("gotoState"), GUIContent.none);

			// Set indent back to what it was
			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty();
		}
	}
}
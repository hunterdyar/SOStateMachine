using UnityEditor;
using UnityEngine;

namespace Bloops.StateMachine.Editor
{
	[CustomEditor(typeof(Machine))]
	public class MachineEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			if(target is Machine machine)
			{
				if (machine != null)
				{
					if (machine.GetCurrent() != null)
					{
						GUILayout.Space(20);
						GUILayout.Label("Current State: " + machine.GetCurrent().name);
					}
				}
			}
		}
	}
}
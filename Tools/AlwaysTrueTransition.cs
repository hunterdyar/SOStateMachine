using UnityEngine;

namespace Bloops.StateMachine
{
	[CreateAssetMenu(fileName = "Transition", menuName = "Bloops/State/TriggerTransition", order = 5)]
	public class AlwaysTrueTransition : Transition
	{
		public override bool CheckConditions()
		{
			return true;
		}
	}
}
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bloops.StateMachine
{
	/// <summary>
	/// An event is just a transition that doesnt need conditions, and instantly triggers the stateMachine it is attached to (that is configured on state machine init).
	/// You may not need this, like if you are triggering the state machine in update.
	/// I want to keep my UIEvents - like closing a dialogue box - tidy, so I made this for a "returnToGameplay" transition.
	/// Its probably a bad idea to have a state machine that uses both update-loop-triggers and event-style-triggers.
	/// </summary>
	
	[CreateAssetMenu(fileName = "Event Transition", menuName = "Bloops/State/Event Transition", order = 4)]
	public class EventTransition : Transition
	{
		private bool invoked = false;
		
		[ContextMenu("Invoke")]
		public void Invoke()
		{
			invoked = true;
			TriggerMachine();
		}

		public override bool CheckConditions()
		{
			//You can make conditional events! You probably did it on accident, but I bet theres some edge case where this is important to support.
			if (_conditions.Count > 0)
			{
				return invoked && base.CheckConditions();
			}
			else
			{
				return invoked;
			}
		}

		public override void OnExit()
		{
			invoked = false;
		}

		public override void Reset()
		{
			invoked = false;
		}
	}
}
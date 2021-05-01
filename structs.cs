using System;

namespace Bloops.StateMachine
{
		
	[Serializable]
	public struct TransitionToState
	{
		public Transition transition;
		public State gotoState;
	}
}
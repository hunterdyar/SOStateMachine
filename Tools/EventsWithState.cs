using UnityEngine;
using UnityEngine.Events;

namespace Bloops.StateMachine
{
	public class EventsWithState : MonoBehaviour, IStateListener
	{
		public State state;
		public UnityEvent OnEnterState;
		public UnityEvent OnExitState;
		void Awake()
		{
			state.AddListener(this);
		}

		private void OnDestroy()
		{
			state.RemoveListener(this);
		}

		public void OnEntry()
		{
			OnEnterState.Invoke();
		}

		public void OnExit()
		{
			OnExitState.Invoke();
		}
	}
}
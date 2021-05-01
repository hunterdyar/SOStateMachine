using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloops.StateMachine
{
	[CreateAssetMenu(fileName = "PuzzleManager", menuName = "Bloops/State/State", order = 4)]

	public class State : ScriptableObject
	{
		[SerializeField] private List<TransitionToState> _transitions;
		private SortedList<int, TransitionToState> transitions = new SortedList<int, TransitionToState>();
		//
		private Machine _machine;
		public bool IsActive => _isActive;
		private bool _isActive = false;
		public Action OnEntryAction;
		public Action OnExitAction;
		public Action OnInitiationAction;
		public readonly List<IStateListener> listeners = new List<IStateListener>();
		public void OnEntry()
		{
			foreach(var tts in transitions)
			{
				tts.Value.transition.OnEntry();
			}

			//
			_isActive = true;
			OnEntryAction?.Invoke();
			//todo this was getting a collection modified error, which makes me things that states arent getting added before start? so? uh? for loop? idfk.
			for (var i = 0; i < listeners.Count; i++)
			{
				listeners[i].OnEntry();
			}
		}

		public void OnExit()
		{
			//
			// foreach(var tts in transitions)
			// {
			// 	tts.Value.transition.OnExit();
			// }
			//
			for (int i = transitions.Values.Count-1; i >= 0; i--)
			{
				transitions.Values[i].transition.OnExit();
			}
			//
			_isActive = false;
			OnExitAction?.Invoke();
			//
			foreach (var l in listeners)
			{
				l.OnExit();
			}
		}

		public void Reset()
		{
			// listeners.Clear();
			foreach (var tts in transitions)
			{
				tts.Value.transition.Reset();
			}
		}
		
		/// <summary>
		/// Returns True if we should exit this state. next is not null, we will go to that state - adding to the stack. if next is null, we will exit this state and return to the previous state before us.
		/// </summary>
		public bool Trigger(out State next)
		{
			//
			foreach (var tts in transitions)
			{
				if (tts.Value.transition.CheckConditions())
				{
					OnExit();
					next = tts.Value.gotoState;
					return true;
				}
			}
			next = null;
			return false;
		}

		public void Init(Machine machine)
		{
			
			_isActive = false;
			_machine = machine;
			transitions.Clear();
			
			foreach (var ttskp in _transitions)
			{
				if (transitions.ContainsKey(ttskp.transition.CheckSortingOrder))
				{
					Debug.LogError($"overlapping sorting orders for {ttskp.transition.name} and {transitions[ttskp.transition.CheckSortingOrder].transition.name}.");
				}
				transitions.Add(ttskp.transition.CheckSortingOrder, ttskp);
				ttskp.transition.SetMachine(_machine);
			}
			//
			OnInitiationAction?.Invoke();
			//
			Reset();
		}

		public void AddTransition(TransitionToState transitionToState)
		{
			transitions.Add(transitionToState.transition.CheckSortingOrder,transitionToState);
		}
		
		public void AddListener(IStateListener listener)
		{
			if (!listeners.Contains(listener))
			{
				listeners.Add(listener);
			}
		}

		public void RemoveListener(IStateListener listener)
		{
			if (listeners.Contains(listener))
			{
				listeners.Remove(listener);
			}
		}
	}
}
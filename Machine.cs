using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Bloops.StateMachine {
	[CreateAssetMenu(fileName = "State Machine", menuName = "Bloops/State/State Machine")]
	public class Machine : ScriptableObject {

		[SerializeField] private State _defaultState;

		public State current => stateStack.Peek();
		private Stack<State> stateStack = new Stack<State>();
		public List<State> states;
	
		//im nto worried about making a visual editor for all of this untl Unity's Graph Tools Foundation is at v 1.0 or some stable-api beta.
		//https://docs.unity3d.com/Packages/com.unity.graphtools.foundation@0.8/manual/index.html
		
		public void Init()
		{
			//from previous scene.
			if (stateStack.Count > 0)
			{
				current.OnExit();
			}

			//
			foreach (var s in states)
			{
				s.Init(this);
			}
			stateStack = new Stack<State>();
		}

		public void StartMachine(bool triggerOnStart = false)
		{
			stateStack.Push(_defaultState);
			_defaultState.OnEntry();
			if (triggerOnStart)
			{
				Trigger();
			}
		}

		public State GetCurrent()
		{
			if (stateStack.Count > 0)
			{
				return stateStack.Peek();
			}

			return null;
		}
		
		public void Trigger() {
			if(current.Trigger(out var next))
			{
				if (next != null)
				{
					next.OnEntry();
					stateStack.Push(next);
				}
				else
				{
					if (stateStack.Count > 1)
					{
						stateStack.Pop();//Get rid of the current - the one we just triggered.
						stateStack.Peek().OnEntry();//The new top of the stack is the new current, and we just transitioned back to it.
					}
				}
			}
		}
	}
}
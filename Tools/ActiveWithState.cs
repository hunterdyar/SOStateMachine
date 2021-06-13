using System;
using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace Bloops.StateMachine
{
	public class ActiveWithState : MonoBehaviour, IStateListener
	{
		public State state;

		void Awake()
		{
			state.AddListener(this);
			gameObject.SetActive(false);
			//the "entry" state is to turn off, and the first state gets an OnEntry, which will enable us.
		}

		private void OnStateInit()
		{
			
		}
		
		private void OnDestroy()
		{
			state.RemoveListener(this);
		}

		public void OnEntry()
		{
			gameObject.SetActive(true);
		}

		public void OnExit()
		{
			gameObject.SetActive(false);
		}
	}
}
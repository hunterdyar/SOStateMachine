﻿using System;
using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace Bloops.StateMachine
{
	public class InactiveWithState : MonoBehaviour, IStateListener
	{
		public State state;

		void Awake()
		{
			state.AddListener(this);
			gameObject.SetActive(true);//the "entry" state is to turn off, and the first state gets an OnEntry, which will enable us.
			// gameObject.SetActive(state.IsActive);//the "entry" state is to turn off, and the first state gets an OnEntry, which will enable us.
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
			gameObject.SetActive(false);
		}

		public void OnExit()
		{
			gameObject.SetActive(true);
		}
	}
}
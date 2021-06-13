using System;
using UnityEngine;

namespace Bloops.StateMachine
{
	public class InitiateMachineOnAwake : MonoBehaviour
	{
		public Machine machine;
		public void Awake()
		{
			machine.Init();
		}
	}
}
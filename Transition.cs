using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bloops.StateMachine
{
	[CreateAssetMenu(fileName = "Transition", menuName = "Bloops/State/Transition", order = 4)]
	///A Transition bject is really a collection of _conditions.

	public class Transition : ScriptableObject
	{
		[SerializeField] private ConditionCheck _checkType;
		private Machine _machine;
		protected List<ICondition> _conditions = new List<ICondition>();
		[Tooltip("Lower numbered transitions get higher priority when the state looks for one that is true. Used to disambiguate when transitions are conflicting")]
		[SerializeField] public int CheckSortingOrder;

		[Header("Conditional scriptableObjects")] [RequireInterface(typeof(ICondition))]
		public ScriptableObject[] conditionObjects;
		//
		public virtual void Reset()
		{
			_conditions.Clear();
			//initialize from array.
			foreach(ICondition condition in conditionObjects)
			{
				AddCondition(condition);
			}
		}
		public void AddCondition(ICondition condition)
		{
			if (!_conditions.Contains(condition))
			{
				_conditions.Add(condition);
			}
		}
		
		public virtual void OnEntry()
		{
			
		}

		public virtual void OnExit()
		{
			
		}
		public void RemoveCondition(ICondition condition)
		{
			if (_conditions.Contains(condition))
			{
				_conditions.Remove(condition);
			}
		}

		public void SetPriority(int priority)
		{
			this.CheckSortingOrder = priority;
		}
		
		public virtual bool CheckConditions()
		{
			if (_conditions.Count == 0)
			{
				return false;
			}

			switch (_checkType)
			{
				case ConditionCheck.any:
					return CheckAnyConditions();
				case ConditionCheck.all:
				default:
					return CheckAllConditions();
			}
		}

		private bool CheckAllConditions()
		{
			for (int i = 0; i < _conditions.Count; i++)
			{
				if (!_conditions[i].GetCondition())
				{
					return false;
				}
			}
			return true;
		}
		private bool CheckAnyConditions()
		{
			for (int i = 0; i < _conditions.Count; i++)
			{
				if (_conditions[i].GetCondition())
				{
					return true;
				}
			}
			return false;
		}

		private enum ConditionCheck
		{
			all,
			any
		}

		public void SetMachine(Machine machine)
		{
			_machine = machine;
		}
		[ContextMenu("Trigger")]
		public void TriggerMachine()
		{
			if (_machine != null)
			{
				_machine.Trigger();
			}
			else
			{
				Debug.LogWarning("Transition has not had its machine set. This may not be an issue, but is needed to trigger the machine.");
			}
		}
	}
}
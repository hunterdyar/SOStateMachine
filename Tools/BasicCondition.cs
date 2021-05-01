namespace Bloops.StateMachine
{
	/// <summary>
	/// Just holds a bool.
	/// </summary>
	public class BasicCondition : ICondition
	{
		private bool condition = true;

		public BasicCondition(bool initial)
		{
			this.condition = initial;
		}

		public void Set(bool condition)
		{
			this.condition = condition;
		}

		public bool GetCondition()
		{
			return condition;
		}
	}
}
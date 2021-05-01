namespace Bloops.StateMachine
{
	public interface IStateListener
	{
		void OnEntry();
		void OnExit();
	}
}
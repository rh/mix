namespace Mix.Core
{
	public interface ITask
	{
		void BeforeAllExecute();

		void Execute(IContext context);

		void AfterAllExecute();
	}
}
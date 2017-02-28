namespace TheLizzards.Data.CQRS.Queries
{
	public interface IWithName<TResult>
	{
		TResult WithName(string name);
	}
}
using System.Threading.Tasks;

namespace TheLizzards.Data.CQRS.Contracts
{
	public interface IStory<T> : IStory
	{
		Task Execute(T payload);
	}

	public interface IStory
	{
	}
}
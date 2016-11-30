using System.Threading.Tasks;
using TheLizzards.Common.Data;

namespace TheLizzards.CQRS.Contracts
{
	public interface IStory<T> : IStory
	{
		Task<IResult> Execute(T payload);
	}

	public interface IStory
	{
	}
}
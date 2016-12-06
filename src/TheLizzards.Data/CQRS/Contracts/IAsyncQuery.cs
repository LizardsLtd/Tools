using System.Threading.Tasks;

namespace TheLizzards.Data.CQRS.Contracts
{
	public interface IAsyncQuery<TPayload> : IQuery<Task<TPayload>>
	{
	}
}
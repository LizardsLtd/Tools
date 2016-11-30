using System.Threading.Tasks;

namespace TheLizzards.CQRS.Contracts
{
	public interface IAsyncQuery<TPayload> : IQuery<Task<TPayload>>
	{
	}
}
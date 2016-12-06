using System.Threading.Tasks;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.CQRS.Contracts.DataAccess
{
	public interface IDataWriter<T>
		where T : IAggregateRoot
	{
		Task Insert(T item);

		Task Update(T item);
	}
}
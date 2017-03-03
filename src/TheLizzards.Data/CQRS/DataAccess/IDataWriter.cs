using System.Threading.Tasks;
using TheLizzards.Data.Domain;

namespace TheLizzards.Data.CQRS.DataAccess
{
	public interface IDataWriter<T>
		where T : IAggregateRoot
	{
		Task InsertNew(T item);

		Task UpdateExisting(T item);
	}
}
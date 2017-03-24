using System.Threading.Tasks;
using Picums.Data.Domain;

namespace Picums.Data.CQRS.DataAccess
{
	public interface IDataWriter<T>
		where T : IAggregateRoot
	{
		Task InsertNew(T item);

		Task UpdateExisting(T item);
	}
}
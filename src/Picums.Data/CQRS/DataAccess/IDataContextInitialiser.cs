using System;
using System.Threading.Tasks;

namespace Picums.Data.CQRS.DataAccess
{
	public interface IDataContextInitialiser : IDisposable
	{
		Task Initialise();
	}
}
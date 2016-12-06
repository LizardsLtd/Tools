using System;
using System.Threading.Tasks;

namespace TheLizzards.Data.CQRS.Contracts.DataAccess
{
	public interface IDataContextInitialiser : IDisposable
	{
		Task Initialise();
	}
}
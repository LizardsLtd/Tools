using System;
using System.Threading.Tasks;

namespace TheLizzards.CQRS.Contracts.DataAccess
{
	public interface IDataContextInitialiser : IDisposable
	{
		Task Initialise();
	}
}
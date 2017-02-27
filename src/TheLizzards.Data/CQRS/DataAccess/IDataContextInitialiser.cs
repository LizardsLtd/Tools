using System;
using System.Threading.Tasks;

namespace TheLizzards.Data.CQRS.DataAccess
{
	public interface IDataContextInitialiser : IDisposable
	{
		Task Initialise();
	}
}
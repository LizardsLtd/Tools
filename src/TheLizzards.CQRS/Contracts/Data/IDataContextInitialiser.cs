using System;
using System.Threading.Tasks;

namespace TheLizzards.CQRS.Contracts.Data
{
	public interface IDataContextInitialiser : IDisposable
	{
		Task Initialise();
	}
}
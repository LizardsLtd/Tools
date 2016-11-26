using System;
using System.Threading.Tasks;

namespace TheLizzards.CQRS
{
	public interface IDataContextInitialiser : IDisposable
	{
		Task Initialise();
	}
}
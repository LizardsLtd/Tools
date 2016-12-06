using System;
using System.Threading.Tasks;

namespace TheLizzards.Data.CQRS.Contracts
{
	public interface ICommandHandler : IDisposable
	{
		bool CanHandle(ICommand command);

		Task Execute(ICommand command);
	}
}
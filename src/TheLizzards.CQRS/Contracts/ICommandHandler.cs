using System;
using System.Threading.Tasks;
using TheLizzards.Common.Data;

namespace TheLizzards.CQRS.Contracts
{
	public interface ICommandHandler : IDisposable
	{
		bool CanHandle(ICommand command);

		Task<IResult> Execute(ICommand command);
	}
}
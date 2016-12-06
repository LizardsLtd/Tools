using System.Threading.Tasks;

namespace TheLizzards.Data.CQRS.Contracts
{
	public interface ICommandBus
	{
		void Dispose();

		Task Execute(ICommand command);
	}
}
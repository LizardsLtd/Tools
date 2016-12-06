using System.Threading.Tasks;

namespace TheLizzards.CQRS.Contracts
{
	public interface ICommandBus
	{
		void Dispose();

		Task Execute(ICommand command);
	}
}
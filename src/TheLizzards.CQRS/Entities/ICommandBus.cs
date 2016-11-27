using System.Threading.Tasks;
using TheLizzards.Common.Data;

namespace TheLizzards.CQRS
{
	public interface ICommandBus
	{
		void Dispose();

		Task<IResult> Execute(ICommand command);
	}
}
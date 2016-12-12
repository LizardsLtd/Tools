using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TheLizzards.Data.CQRS.Contracts
{
	public interface ICommandBus
	{
		void Dispose();

		IEnumerable<ValidationResult> Validate(ICommand command);

		Task Execute(ICommand command);
	}
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TheLizzards.Data.CQRS
{
	public interface ICommandBus
	{
		void Dispose();

		Task<IEnumerable<ValidationResult>> Validate(ICommand command);

		Task Execute(ICommand command);
	}
}
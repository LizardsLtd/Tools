using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheLizzards.Data.Types.Services {
	public static class ValidationResultConverter {
		public static ValidationResult ToValidationResult(this bool isSucess, string messageWhenFalse)
			=> isSucess
				? ValidationResult.Success
				: new ValidationResult(messageWhenFalse);
	}
}

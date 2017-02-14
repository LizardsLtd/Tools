﻿using System.ComponentModel.DataAnnotations;

namespace TheLizzards.Data.Types.Services
{
	public static class ValidationResultConverter
	{
		public static ValidationResult ToValidationResult(this bool isSucess, string messageWhenFalse)
			=> isSucess
				? ValidationResult.Success
				: new ValidationResult(messageWhenFalse);
	}
}
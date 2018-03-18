//using System;
//using System.Linq;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc.Localization;

//namespace Picums.Web
//{
//	public static class IdentityErrorExtension
//	{
//		public static IdentityError ToIdentityError(
//			this Exception exception
//			, IHtmlLocalizer translator)
//		{
//			var message = translator
//				.GetString($"Exceptions.{exception.GetType().Name}");

//			return CreateIdentityError(exception.Code, message);
//		}

//		public static IdentityError ToIdentityError(this string message, string code)
//			=> CreateIdentityError(code, message);

//		//public static IdentityError ToIdentityError(this IResult result)
//		//	=> result
//		//		.ErrorMessages
//		//		.Select(x => new IdentityError
//		//		{
//		//			Code = x,
//		//			Description = x
//		//		})
//		//		.First();

//		//public static IdentityResult ToIdentityResult(this IResult result)
//		//	=> IdentityResult.Failed(
//		//		result
//		//			.ToIdentityError());

//		private static IdentityError CreateIdentityError(int code, string message)
//			=> CreateIdentityError(code.ToString(), message);

//		private static IdentityError CreateIdentityError(string code, string message)
//			=> new IdentityError
//			{
//				Code = code,
//				Description = message
//			};
//	}
//}
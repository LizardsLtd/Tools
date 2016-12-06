using TheLizzards.Data.Types.Entites;
using TheLizzards.Mvc.Data.Types.ApplicationServices;
using TheLizzards.Mvc.Data.Types.ModelValidators;
using TheLizzards.Mvc.Startup;

namespace TheLizzards.Mvc
{
	public static class DataPartsBootstrapperExtension
	{
		public static IConfiguration AddDataParts(this IConfiguration startup)
			=> startup
				.AddBankModelHandlers()
				.AddEmailModelHandlers()
				.AddAddressModelHandlers();

		public static IConfiguration AddBankModelHandlers(this IConfiguration startup)
			=> startup
				.ForMvcOption()
					.AddModelBinderProvider<BankDetails, BankDetailsModelBinder>()
					.AddModelValidator<BankDetailsModelValidatorProvider>();

		public static IConfiguration AddEmailModelHandlers(this IConfiguration startup)
			=> startup
				.ForMvcOption()
					.AddModelBinderProvider<Email, EmailModelBinder>();

		public static IConfiguration AddAddressModelHandlers(this IConfiguration startup)
			=> startup
				.ForMvcOption()
					.AddModelBinderProvider<Address, AddressModelBinder>();
	}
}
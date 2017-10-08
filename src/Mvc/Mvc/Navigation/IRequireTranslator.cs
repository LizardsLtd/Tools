using Microsoft.Extensions.Localization;

namespace Picums.Mvc.Navigation
{
	public interface IRequireTranslator<out TResult>
	{
		TResult WithTranslator(IStringLocalizer translator);
	}
}
using Microsoft.Extensions.Localization;

namespace TheLizzards.Mvc.Navigation
{
	public interface IRequireTranslator<out TResult>
	{
		TResult WithTranslator(IStringLocalizer translator);
	}
}
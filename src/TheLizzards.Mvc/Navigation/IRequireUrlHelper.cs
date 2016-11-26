using Microsoft.AspNetCore.Mvc;

namespace TheLizzards.Mvc.Navigation
{
	public interface IRequireUrlHelper<out TResult>
	{
		TResult WithUrlHelper(IUrlHelper helper);
	}
}
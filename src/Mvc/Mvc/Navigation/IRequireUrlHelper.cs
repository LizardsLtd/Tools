using Microsoft.AspNetCore.Mvc;

namespace Picums.Mvc.Navigation
{
	public interface IRequireUrlHelper<out TResult>
	{
		TResult WithUrlHelper(IUrlHelper helper);
	}
}
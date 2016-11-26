using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace TheLizzards.Mvc.Middleware
{
	internal sealed class CultureCookieSetterMiddleware
	{
		private readonly RequestDelegate next;

		public CultureCookieSetterMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			this.CreateLanguageCookie(context);
			await this.next.Invoke(context);
		}

		private void CreateLanguageCookie(HttpContext context)
		{
			var currentCluture =
				context
				.Features[typeof(IRequestCultureFeature)] as IRequestCultureFeature;

			var cookieValue =
				CookieRequestCultureProvider.MakeCookieValue(currentCluture?.RequestCulture);

			context.Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName
				, cookieValue
				, new CookieOptions
				{
					Expires = DateTimeOffset.Now.AddDays(30).DateTime
				});
		}
	}
}
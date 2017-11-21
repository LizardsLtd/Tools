using Markdig;
using Microsoft.AspNetCore.Mvc.Localization;

namespace Picums.Mvc.View
{
    public static class CommonMarkExtension
    {
        public static LocalizedHtmlString GetCommonMark(
            this IHtmlLocalizer localiser,
            string key,
            params object[] attributes)
        {
            var translatedString = localiser.GetString(key, attributes);

            var html = Markdown.ToHtml(translatedString);

            return new LocalizedHtmlString(key, html);
        }
    }
}
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Localization;
//using Microsoft.Extensions.DependencyInjection;

//namespace Microsoft.AspNetCore.Builder
//{
//    public static class BuilderExtensions
//    {
//        public static IApplicationBuilder UseDefaultLocalisation(this IApplicationBuilder app)
//        {
//            var cultures = app
//                .ApplicationServices
//                .GetService<CultureStore>();

//            var availableCultures = cultures.AvailableCultures.ToList();

//            return app
//                .UseRequestLocalization(
//                    new RequestLocalizationOptions
//                    {
//                        RequestCultureProviders = new List<IRequestCultureProvider>
//                        {
//                            new AcceptLanguageHeaderRequestCultureProvider()
//                            , new QueryStringRequestCultureProvider()
//                            , new CookieRequestCultureProvider()
//                        },
//                        SupportedCultures = availableCultures,
//                        SupportedUICultures = availableCultures,
//                        DefaultRequestCulture = new RequestCulture(cultures.DefaultCulture),
//                    });
//        }
//    }
//}
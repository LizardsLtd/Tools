using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TheLizzards.Mvc.Startup.Mvc
{
    public class MvcServices
    {
        private readonly List<Action<MvcOptions>> mvcOptionsActions;

        public MvcServices()
        {
            this.mvcOptionsActions = new List<Action<MvcOptions>>(15);
        }

        //internal void SetupMvcService(IServiceCollection services)
        //{
        //    var mvcBuilder = services
        //        .AddMvc()
        //        .AddViewLocalization();
        //    //.AddDataAnnotationsLocalization(this.DataAnnotationOptions);

        //    services
        //        .AddSingleton(AddNavigationItems(services, mvcBuilder))
        //        .AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        //}

        //private void DataAnnotationOptions(MvcDataAnnotationsLocalizationOptions options)
        //{
        //    options.DataAnnotationLocalizerProvider = (x, y) => this.localiser;
        //}
    }
}
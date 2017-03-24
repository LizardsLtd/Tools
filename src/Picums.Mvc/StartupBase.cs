using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Picums.Mvc.Startup;

namespace Picums.Web
{
    public abstract class StartupBase : IConfiguration
    {
        private readonly List<Action<IServiceCollection>> serviceConfigurationAction;
        private readonly List<Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory>> configurationAction;

        private readonly IHostingEnvironment env;

        private readonly MvcConfiguration mvcConfiguration;

        public StartupBase(IHostingEnvironment env)
        {
            this.env = env;
            this.serviceConfigurationAction = new List<Action<IServiceCollection>>(15);
            this.configurationAction = new List<Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory>>(15);
            //this.mvcConfiguration = new MvcConfiguration(this);
        }

        //public IConfiguration AddServices(Action<IServiceCollection> action)
        //{
        //    this.serviceConfigurationAction.Add(action);
        //    return this;
        //}

        //public IConfiguration ConfigureOption<TOption>(Action<TOption> action)
        //    where TOption : class
        //{
        //    this.serviceConfigurationAction.Add(x => x.Configure(action));
        //    return this;
        //}

        //public IConfiguration AddConfiguration(Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory> action)
        //{
        //    this.configurationAction.Add(action);
        //    return this;
        //}

        //public MvcConfiguration ForMvcOption() => this.mvcConfiguration;

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    this.serviceConfigurationAction.ForEach(action => action(services));

        //    var mvcServices = new MvcServices();
        //    ApplyMvcServices(mvcServices);
        //    mvcServices.SetupMvcService(services);
        //}

        //public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        //{
        //    var mvcConfiguration = this.ConfigureMvc(app);

        //    this.configurationAction.ForEach(action => action(app, this.env, loggerFactory));

        //    mvcConfiguration.SetupUseMvc(app);
        //}

        //protected abstract void ApplyMvcConfiguration(MvcConfiguration mvcConfiguration);

        //protected abstract void ApplyMvcServices(MvcServices mvcServices);

        //private MvcConfiguration ConfigureMvc(IApplicationBuilder app)
        //{
        //    var localiser = app.ApplicationServices.GetService<IStringLocalizer>();
        //    var mvcConfiguration = new MvcConfiguration(localiser);

        //    this.ApplyMvcConfiguration(mvcConfiguration);

        //    return mvcConfiguration;
        //}
    }
}
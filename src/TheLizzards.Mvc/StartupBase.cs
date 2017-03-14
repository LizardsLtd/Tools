using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TheLizzards.Mvc.Startup;

namespace Picums.Web
{
    public abstract class StartupBase : IConfiguration
    {
        private readonly List<Action<IServiceCollection>> serviceConfigurationAction;
        private readonly List<Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory>> configurationAction;

        private readonly List<Action<IServiceProvider>> setupSystemAfterInitialisationActions;
        private readonly IHostingEnvironment env;

        private readonly MvcConfiguration mvcConfiguration;

        public StartupBase(IHostingEnvironment env)
        {
            this.env = env;
            this.serviceConfigurationAction = new List<Action<IServiceCollection>>(15);
            this.configurationAction = new List<Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory>>(15);
            this.setupSystemAfterInitialisationActions = new List<Action<IServiceProvider>>(15);
            this.mvcConfiguration = new MvcConfiguration(this);
        }

        public IConfiguration AddServices(Action<IServiceCollection> action)
        {
            this.serviceConfigurationAction.Add(action);
            return this;
        }

        public IConfiguration ConfigureOption<TOption>(Action<TOption> action)
            where TOption : class
        {
            this.serviceConfigurationAction.Add(x => x.Configure(action));
            return this;
        }

        public IConfiguration AddConfiguration(Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory> action)
        {
            this.configurationAction.Add(action);
            return this;
        }

        public IConfiguration AddSetupSystemAfterInitialisation(Action<IServiceProvider> action)
        {
            this.setupSystemAfterInitialisationActions.Add(action);
            return this;
        }

        public MvcConfiguration ForMvcOption() => this.mvcConfiguration;

        public void ConfigureServices(IServiceCollection services, ILoggerFactory loggerFactory)
        {
            this.serviceConfigurationAction.ForEach(action => action(services));
            this.mvcConfiguration.SetupMvcService(services);
        }

        public virtual void InitialiseBeforeConfigure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            this.InitialiseBeforeConfigure(app, loggerFactory);
            this.configurationAction.ForEach(action => action(app, this.env, loggerFactory));
            this.setupSystemAfterInitialisationActions.ForEach(action => action(app.ApplicationServices));
            this.mvcConfiguration.SetupUseMvc(app);
        }
    }
}
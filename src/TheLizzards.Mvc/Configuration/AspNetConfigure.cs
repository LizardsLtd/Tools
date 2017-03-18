using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class AspNetConfigure
    {
        internal IHostingEnvironment Enviroment { get; }

        internal AspNetConfigure(IHostingEnvironment env, ConfigurationBuilder configurationProvider)
        {
            Enviroment = env;

            Configuration = configurationProvider.Build();

            ServiceRegistry = new ServiceRegistry();
            MvcRegistry = new MvcRegistry();
            AspRegistry = new AspRegistry();
            TranslationRegistry = new TranslationRegistry();
        }

        public ServiceRegistry ServiceRegistry { get; }

        public MvcRegistry MvcRegistry { get; }

        public AspRegistry AspRegistry { get; }

        public TranslationRegistry TranslationRegistry { get; }

        public IConfigurationRoot Configuration { get; }

        internal void ConfigureServices(IServiceCollection services)
        {
            ServiceRegistry.Execute(services);
            MvcRegistry.AddMvc(services);
        }

        internal void ConfigureAll(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            AspRegistry.Configure(app, Enviroment, loggerFactory);
            MvcRegistry.UseMvc();
        }

        internal void SetupEnviromentType(
            IApplicationBuilder app
            , Action<IApplicationBuilder> configureDevelopmentEnviroment
            , Action<IApplicationBuilder> configureStagingEnviroment
            , Action<IApplicationBuilder> configureProductionEnviroment)
        {
            if (Enviroment.IsDevelopment())
            {
                configureDevelopmentEnviroment(app);
            }
            else if (Enviroment.IsStaging())
            {
                configureStagingEnviroment(app);
            }
            else if (Enviroment.IsProduction())
            {
                configureProductionEnviroment(app);
            }
        }
    }
}
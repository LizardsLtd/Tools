using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Picums.Console
{
    public sealed class Application<TApplication>
        where TApplication : class, IRunnable
    {
        public Application()
        {
            this.ServiceCollection = new ServiceCollection();

            this.ServiceCollection.AddTransient<IRunnable, TApplication>();
            this.ServiceCollection.AddOptions();

            this.Configuration
                = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory());
            this.ConfigurationRoot = new Lazy<IConfigurationRoot>(() => this.Configuration.Build());
        }

        public IConfigurationBuilder Configuration { get; }

        public Lazy<IConfigurationRoot> ConfigurationRoot { get; }

        public ServiceCollection ServiceCollection { get; }

        public void Run()
        {
            var serviceProvider = this.ServiceCollection.BuildServiceProvider();
            serviceProvider.GetService<IRunnable>().Run();
        }

        public Task RunAsync()
            => Task.Run(() =>
            {
                var serviceProvider = this.ServiceCollection.BuildServiceProvider();
                var cancelationToken = new CancellationTokenSource();
                serviceProvider.GetService<IAsyncRunnable>().Run(cancelationToken.Token);
            });
    }
}
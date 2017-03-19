using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TheLizzards.Mvc.Configuration.Defaults
{
    public interface IDefaultsHost
    {
        MvcRegistry MVC { get; }

        AspRegistry ASP { get; }

        IHostingEnvironment Environment { get; }

        IConfigurationRoot ConfigurationRoot { get; }
    }
}
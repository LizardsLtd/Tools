using Microsoft.Extensions.DependencyInjection;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class DataPartsDefault : BasicDefault
    {
        protected override void ConfigureServices(IServiceCollection services, object[] arguments)
        {
            services.AddSingleton(new DatabaseParts(arguments[0].ToString(), arguments[1].ToString()));
        }
    }
}
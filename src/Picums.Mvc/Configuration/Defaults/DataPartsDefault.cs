using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class DataPartsDefault : BasicDefault
    {
        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
        {
            services.AddSingleton(new DatabaseParts(arguments.ElementAt(0).ToString(), arguments.ElementAt(1).ToString()));
        }
    }
}
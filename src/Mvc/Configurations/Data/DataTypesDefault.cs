using System.Collections.Generic;
using Picums.Data.Types;
using Picums.Mvc.ApplicationServices;
using Picums.Mvc.Configuration.Defaults;

namespace Picums.Mvc.Configuration.Data
{
    public sealed class DataTypesDefault : IDefault
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            host.MVC.Options
                .AddModelBinderProvider<Address, AddressModelBinder>()
                .AddModelBinderProvider<BankDetails, BankDetailsModelBinder>()
                .AddModelBinderProvider<Email, EmailModelBinder>();
        }
    }
}
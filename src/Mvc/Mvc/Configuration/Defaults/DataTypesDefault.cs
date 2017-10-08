using System.Collections.Generic;
using Picums.Data.Types;
using Picums.Mvc.ApplicationServices;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class DataTypesDefault : IDefault
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            host.MVC.Models
                .AddModelBinderProvider<Address, AddressModelBinder>()
                .AddModelBinderProvider<BankDetails, BankDetailsModelBinder>()
                .AddModelBinderProvider<Email, EmailModelBinder>();
        }
    }
}
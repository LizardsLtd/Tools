using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class ModelBinderDefault<TBindedType, TModelBinder> : IDefault
        where TModelBinder : IModelBinder, new()
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
            => host.MVC.Options.AddModelBinderProvider<TBindedType, TModelBinder>();
    }
}
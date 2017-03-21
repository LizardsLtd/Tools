using Microsoft.Extensions.DependencyInjection;
using TheLizzards.I18N.Data;
using TheLizzards.I18N.Data.Services;

namespace TheLizzards.Mvc.Configuration.Defaults
{
    public sealed class LocalisationByDatabase : IDefault
    {
        public void Apply(StartupConfigurations host, params object[] arguments)
        {
            host.Services.Add(x => x.AddTransient<GetAllTranslationsQuery>());
            host.Services.Add(x => x.AddTransient<ITranslationSetProvider, DataTranslationProvider>());
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Picums.Data.CQRS;
using Picums.Localisation.Data;
using Picums.Localisation.Data.Services;

namespace Picums.Localisation
{
    public sealed class ReloadTranslationsCommandHandler : CommandHandlerBase<ReloadTranslationsCommand>
    {
        private readonly IServiceCollection services;
        private readonly IStringLocalizer localiser;
        private readonly IEnumerable<ITranslationSetProvider> translationData;
        private readonly CultureStore store;

        public ReloadTranslationsCommandHandler(IStringLocalizer localiser, IEnumerable<ITranslationSetProvider> translationData, CultureStore store)
        {
            this.services = services;
            this.localiser = localiser;
            this.translationData = translationData;
            this.store = store;
        }

        protected override Task Execute(ReloadTranslationsCommand command)
        {
            var loclaiserIndex = services.IndexOf(new ServiceDescriptor(typeof(IStringLocalizer), this.localiser));

            services.RemoveAt(loclaiserIndex);
            services.AddSingleton(new ConfigurableStringLocalizer(this.translationData, this.store));

            return Task.CompletedTask;
        }
    }
}
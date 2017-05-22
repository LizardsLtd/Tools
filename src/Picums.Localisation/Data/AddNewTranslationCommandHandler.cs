using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Localisation.Data
{
    public sealed class AddNewTranslationCommandHandler : CommandHandlerBase<AddNewTranslationCommand>
    {
        private readonly IDataContext storageContext;
        private readonly DatabaseParts parts;
        private readonly FindTranslationByKeyQuery query;

        public AddNewTranslationCommandHandler(IDataContext storageContext, ILoggerFactory loggerFactory, DatabaseParts parts)
        {
            this.storageContext = storageContext;
            this.parts = parts;

            this.query = new FindTranslationByKeyQuery(storageContext, loggerFactory, parts);
        }

        protected override async Task Execute(AddNewTranslationCommand command)
        {
            var translationItem = await GetTranslationItem(command);

            await this.storageContext
                .GetWriter<TranslationItem>(this.parts)
                .InsertNew(translationItem);
        }

        private async Task<TranslationItem> GetTranslationItem(AddNewTranslationCommand command)
        {
            var existingTranslationItem = await this.query.GetByKey(command.TranslationItem);

            return existingTranslationItem.IsSome
                ? existingTranslationItem.Value.AddValue(command.TranslationItem.Value)
                : command.TranslationItem;
        }
    }
}
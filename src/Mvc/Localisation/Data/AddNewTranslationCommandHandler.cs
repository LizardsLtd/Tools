using System.Threading.Tasks;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Mvc.Localisation.DataStorage
{
    public sealed class AddNewTranslationCommandHandler : ICommandHandler<AddNewTranslationCommand>
    {
        private readonly IDataContext storageContext;
        private readonly DatabaseParts parts;
        private readonly FindTranslationByKeyQuery query;

        public AddNewTranslationCommandHandler(IDataContext storageContext, ILogger logger, DatabaseParts parts)
        {
            this.storageContext = storageContext;
            this.parts = parts;

            this.query = new FindTranslationByKeyQuery(storageContext, logger, parts);
        }

        public void Dispose()
        {
        }

        public async Task Handle(AddNewTranslationCommand command)
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
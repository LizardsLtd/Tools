using System.Threading.Tasks;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Mvc.Localisation.DataStorage
{
    public sealed class AddNewTranslationCommandHandler : ICommandHandler<AddNewTranslationCommand>
    {
        private readonly IDataContext storageContext;
        private readonly FindTranslationByKeyQuery query;

        public AddNewTranslationCommandHandler(IDataContext storageContext, ILogger logger)
        {
            this.storageContext = storageContext;
            this.query = new FindTranslationByKeyQuery(storageContext, logger);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public async Task Handle(AddNewTranslationCommand command)
        {
            var translationItem = await this.GetTranslationItem(command);

            await this.storageContext
                .GetWriter<TranslationItem>()
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
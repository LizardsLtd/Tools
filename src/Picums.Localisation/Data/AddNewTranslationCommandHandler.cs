using System.Threading.Tasks;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Localisation.Data
{
    public sealed class AddNewTranslationCommandHandler : CommandHandlerBase<AddNewTranslationCommand>
    {
        private readonly IDataContext storageContext;
        private readonly DatabaseParts parts;

        public AddNewTranslationCommandHandler(IDataContext storageContext, DatabaseParts parts)
        {
            this.storageContext = storageContext;
            this.parts = parts;
        }

        protected override async Task Execute(AddNewTranslationCommand command)
            => await this.storageContext
                .GetWriter<TranslationItem>(new DatabaseParts(command.DatabaseName, "Translations"))
                .InsertNew(command.TranslationItem);
    }
}
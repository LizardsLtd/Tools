using Picums.Data.CQRS;

namespace Picums.Localisation.Data
{
    public sealed class AddNewTranslationCommand : CommandBase, ICommand
    {
        /// <summary>Record Constructor</summary>
        /// <param name="translationItem"><see cref="TranslationItem"/></param>
        /// <param name="databaseName"><see cref="DatabaseName"/></param>
        public AddNewTranslationCommand(TranslationItem translationItem, string databaseName)
        {
            this.TranslationItem = translationItem;
            this.DatabaseName = databaseName;
        }

        public TranslationItem TranslationItem { get; }

        public string DatabaseName { get; }
    }
}
using Picums.Data.CQRS;

namespace Picums.Mvc.Localisation.DataStorage
{
    public sealed class AddNewTranslationCommand : CommandBase, ICommand
    {
        /// <summary>Record Constructor</summary>
        /// <param name="translationItem"><see cref="TranslationItem"/></param
        /// <param name="databaseName"><see cref="DatabaseName"/></param>
        public AddNewTranslationCommand(TranslationItem translationItem)
        {
            this.TranslationItem = translationItem;
        }

        public TranslationItem TranslationItem { get; }
    }
}
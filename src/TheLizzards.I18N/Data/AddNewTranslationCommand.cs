using TheLizzards.Data.CQRS.Commands;

namespace TheLizzards.I18N.Data
{
    public sealed class AddNewTranslationCommand : CommandBase
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

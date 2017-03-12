namespace TheLizzards.I18N.Data.Services
{
    internal sealed class DataTransaltionProvider
    {
        private readonly GetAllTranslationsQuery query;

        public DataTransaltionProvider(GetAllTranslationsQuery query)
        {
            this.query = query;
        }

        public TranslationSet GetTranslationSet()
            => new TranslationSet(query.GetQuery().Execute().GetAwaiter().GetResult());
    }
}
namespace Picums.Localisation.Data.Services
{
    public sealed class DataTranslationProvider : ITranslationSetProvider
    {
        private readonly GetAllTranslationsQuery query;

        public DataTranslationProvider(GetAllTranslationsQuery query)
        {
            this.query = query;
        }

        public TranslationSet GetTranslationSet()
            => new TranslationSet(query.GetQuery().Execute().GetAwaiter().GetResult());
    }
}
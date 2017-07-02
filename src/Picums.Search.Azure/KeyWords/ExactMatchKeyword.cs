namespace Picums.Search.Azure.KeyWords
{
    public sealed class ExactMatchKeyword : ISearchForParameter
    {
        private readonly string keyword;

        public ExactMatchKeyword(string keyword)
        {
            this.keyword = keyword;
        }

        public string GetSearchCommmand()
            => this.keyword;
    }
}
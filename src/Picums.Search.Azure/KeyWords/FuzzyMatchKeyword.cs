namespace Picums.Search.Azure.KeyWords
{
    public sealed class FuzzyMatchKeyword : ISearchForParameter
    {
        private readonly string keyword;

        public FuzzyMatchKeyword(string keyword)
        {
            this.keyword = keyword;
        }

        public string GetSearchCommmand()
            => $"{this.keyword}*";
    }
}
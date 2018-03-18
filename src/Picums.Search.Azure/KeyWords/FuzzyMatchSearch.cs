namespace Picums.Search.Azure.KeyWords
{
    public sealed class FuzzyMatchSearch : ISearch
    {
        private readonly string keyword;

        public FuzzyMatchSearch(string keyword)
        {
            this.keyword = keyword;
        }

        public string GetSearchText() => $"{this.keyword}*";
    }
}
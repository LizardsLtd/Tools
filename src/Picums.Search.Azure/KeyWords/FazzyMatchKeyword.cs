namespace Picums.Search.Azure.KeyWords
{
    public sealed class FazzyMatchKeyword : ISearchForParameter
    {
        private readonly string keyword;

        public FazzyMatchKeyword(string keyword)
        {
            this.keyword = keyword;
        }

        public string GetSearchCommmand()
            => $"*{this.keyword}*";
    }
}
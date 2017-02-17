namespace TheLizzards.Search.Entities
{
	public sealed class TextSearchKeyWord : ISearchKey
	{
		public TextSearchKeyWord(string searchFor)
		{
			this.SearchTokens = searchFor;
		}

		public string SearchTokens { get; }
	}
}
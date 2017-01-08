namespace TheLizzards.Search
{

	public sealed class StringKeyWord : IKeyWord
	{
		public StringKeyWord(string searchFor)
		{
			this.SearchTokens = searchFor.Split(' ');
		}

		public string[] SearchTokens { get; }

		public string Combine() => string.Join(" ", this.SearchTokens);
	}
}

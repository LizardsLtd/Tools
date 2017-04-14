namespace Picums.Search.Azure.KeyWords
{
	public sealed class TextSearchForParameter : ISearchForParameter
	{
		private readonly string keyword;

		public TextSearchForParameter(string keyword)
		{
			this.keyword = keyword;
		}

		public string GetSearchCommmand()
			=> this.keyword;
	}
}
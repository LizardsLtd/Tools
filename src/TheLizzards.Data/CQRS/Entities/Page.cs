namespace TheLizzards.Data.CQRS.Entities
{
	public sealed class Page
	{
		private Page(int countOfItemsToSkip, int itemsPerPage)
		{
			this.ItemsOnPage = itemsPerPage;
			this.CountOfItemsToSkip = countOfItemsToSkip;
		}

		public int CountOfItemsToSkip { get; }

		public int ItemsOnPage { get; }

		public static Page Current(int pageNumber, int itemsPerPage = 10)
			=> new Page(pageNumber * itemsPerPage, itemsPerPage);

		public Page Next() => new Page(CountOfItemsToSkip + ItemsOnPage, ItemsOnPage);

		public Page Prev() => new Page(CountOfItemsToSkip - ItemsOnPage, ItemsOnPage);
	}
}
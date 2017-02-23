namespace TheLizzards.Data.Queries
{
	public sealed class DatabaseParts
	{
		private readonly string database;
		private readonly string collection;

		public DatabaseParts(string database, string collection)
		{
			this.database = database;
			this.collection = collection;
		}

		public object[] Parts => new[] { this.database, this.collection };
	}
}
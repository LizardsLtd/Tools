namespace Picums.Data.CQRS.DataAccess
{
    public sealed class DatabaseParts
    {
        public DatabaseParts(string database, string collection)
        {
            this.Database = database;
            this.Collection = collection;
        }

        public string Database { get; }

        public string Collection { get; }

        public DatabaseParts WithCollection(string collection) => new DatabaseParts(this.Database, collection);
    }
}
namespace TheLizzards.Data.CQRS.DataAccess
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

        public static implicit operator object[] (DatabaseParts parts) => parts.Parts;

        public DatabaseParts WithCollection(string collection) => new DatabaseParts(this.database, collection);
    }
}
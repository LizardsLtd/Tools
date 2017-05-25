namespace Picums.Data.CQRS.Queries
{
    public interface IQueryBuilder<TResult>
        : IWithDataContext<IWithLogger<IWithDatabaseParts<TResult>>>
        , IWithLogger<IWithDatabaseParts<TResult>>
        , IWithDatabaseParts<TResult>
    { }
}
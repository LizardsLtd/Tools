using Microsoft.Azure.Search.Models;

namespace Picums.Search.Azure
{
    public interface ISearchResultFactory<TItem> where TItem : IHasScore
    {
        string[] Fields { get; }

        TItem Create(double score, Document document);
    }
}
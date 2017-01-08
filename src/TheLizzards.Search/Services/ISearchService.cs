using System.Threading.Tasks;
using TheLizzards.Search.Entities;

namespace TheLizzards.Search.Services
{
	public interface ISearchService<TKey, TResult>
		where TKey : ISearchKey
		where TResult : ISearchResult
	{
		Task<SearchResults<TResult>> SearchFor(TKey keyword);
	}
}

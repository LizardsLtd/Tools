using System.Threading.Tasks;
using TheLizzards.Search.Entities;

namespace TheLizzards.Search.Services
{
	public interface ISearchService<T>
		where T: ISearchResult
	{
		Task<SearchResults<T>> SearchFor(IKeyWord keyword);
	}
}

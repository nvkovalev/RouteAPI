using RouteAPI.Requests;
using RouteAPI.Responses;

namespace RouteAPI.Interfaces;

public interface ISearchService
{
    Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken);
    Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
}

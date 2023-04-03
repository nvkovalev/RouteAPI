using RouteAPI.Requests;
using RouteAPI.Responses;

using Route = RouteAPI.Responses.Route;

namespace RouteAPI.Interfaces
{
    public interface ICacheService
    {
        void Add(SearchResponse response);
        Route Get(Guid id);
        Route[] Get(SearchRequest searchRequest);
        void Clear();
    }
}

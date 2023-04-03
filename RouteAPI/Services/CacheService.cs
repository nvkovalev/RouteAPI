using RouteAPI.Interfaces;
using RouteAPI.Requests;
using RouteAPI.Responses;

using Route = RouteAPI.Responses.Route;

namespace RouteAPI.Services
{
    public class CacheService : ICacheService
    {
        public CacheService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        //we can use otther types of caching
        private readonly Dictionary<Guid, Route> _cache = new Dictionary<Guid, Route>();
        private readonly IDateTimeProvider _dateTimeProvider;

        public void Add(SearchResponse response)
        {
            Array.ForEach(response.Routes, route => _cache.Add(route.Id, route));
        }

        public void Clear()
        {
            var expiredRoutes = _cache.Values
                .Where(route => route.TimeLimit <= _dateTimeProvider.Now);

            foreach(var route in expiredRoutes)
            {
                _cache.Remove(route.Id);
            }
        }

        public Route Get(Guid id)
        {
            if (_cache.TryGetValue(id, out var route))
            {
                return route;
            }

            return null;
        }

        public Route[] Get(SearchRequest searchRequest)
        {
            return _cache.Values.Where(route =>
                 route.Destination == searchRequest.Destination
                 && route.Origin == searchRequest.Origin
                 && route.OriginDateTime == searchRequest.OriginDateTime
                 
                 // we can add more conditions here
            ).ToArray();
        }
    }
}

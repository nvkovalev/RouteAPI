using MediatR;
using RouteAPI.Interfaces;
using RouteAPI.Requests;
using RouteAPI.Responses;

namespace RouteAPI.Pipelines
{
    public class CacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : SearchRequestMain
        where TResponse : SearchResponse
    {
        private readonly ICacheService _cacheService;

        public CacheBehavior(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.Filters != null && request.Filters.OnlyCached.HasValue && request.Filters.OnlyCached.Value)
            {
                // need to clear expired routes
                _cacheService.Clear();

                // Or maybe we have to get by Id ?
                var routes = _cacheService.Get(request);

                if (routes == null)
                    throw new Exception("No items found in cache");

                var result = new SearchResponse
                {
                    Routes = routes,
                    MaxMinutesRoute = routes.Max(x => (int)(x.DestinationDateTime - x.OriginDateTime).TotalMinutes),
                    MinMinutesRoute = routes.Min(x => (int)(x.DestinationDateTime - x.OriginDateTime).TotalMinutes),
                    MaxPrice = routes.Max(x => x.Price),
                    MinPrice = routes.Min(x => x.Price)
                };

                return result as TResponse;
            }

            var response = await next();

            _cacheService.Add(response);

            return response;
        }
    }
}

using MediatR;
using RouteAPI.Interfaces;
using RouteAPI.Requests;
using RouteAPI.Responses;

namespace RouteAPI.RequestHandlers
{
    public class SearchRequestHandler : IRequestHandler<SearchRequestMain, SearchResponse>
    {
        private readonly IEnumerable<ISearchService> _searchServices;

        public SearchRequestHandler(IEnumerable<ISearchService> searchServices)
        {
            _searchServices = searchServices;
        }

        public async Task<SearchResponse> Handle(SearchRequestMain request, CancellationToken cancellationToken)
        {
            var responses = new List<SearchResponse>();

            foreach (var service in _searchServices.Where(x => request.AvailableServices.Contains(x.GetType().FullName)))
            {
                responses.Add(await service.SearchAsync(request, cancellationToken));
            }

            return new SearchResponse
            {
                Routes = responses.SelectMany(x => x.Routes).ToArray(),
                MaxMinutesRoute = responses.Max(x => x.MaxMinutesRoute),
                MinMinutesRoute = responses.Min(x => x.MinMinutesRoute),

                MaxPrice = responses.Max(x => x.MaxPrice),
                MinPrice = responses.Min(x => x.MinPrice),
            };
        }
    }
}

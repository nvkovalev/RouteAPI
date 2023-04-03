using AutoMapper;
using RouteAPI.Interfaces;
using RouteAPI.Options;
using RouteAPI.Requests;
using RouteAPI.Responses;
using System.Text;
using System.Text.Json;

namespace RouteAPI.Services
{
    public abstract class SearchServiceBase<TRequest, TResponse> : ISearchService
        where TRequest : class
        where TResponse : class
    {
        private readonly IMapper _mapper;

        private readonly Uri _pingUrl;
        private readonly Uri _searchUrl;

        protected abstract string providerName { get; }

        public SearchServiceBase(IMapper mapper, Providers providers)
        {
            _mapper = mapper;

            var options = providers.Options.First(x => x.Name == providerName);

            _pingUrl = new Uri($"{options.ApiUrl}{options.PingEndpoint}");
            _searchUrl = new Uri($"{options.ApiUrl}{options.SearchEndpoint}");
        }


        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(_pingUrl, cancellationToken);
                return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                using var httpClient = new HttpClient();

                var json = JsonSerializer.Serialize(_mapper.Map<TRequest>(request));

                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"
                );

                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = _searchUrl,
                    Content = content
                };

                var httpResponse = await httpClient.SendAsync(httpRequest);

                var responseContent = await httpResponse.Content.ReadAsStringAsync();

                var response = JsonSerializer.Deserialize<TResponse>(responseContent);

                return _mapper.Map<SearchResponse>(response);
            }
            catch (Exception ex)
            {
                // we can throw here our type of exception
                throw ex;
            }
        }
    }
}

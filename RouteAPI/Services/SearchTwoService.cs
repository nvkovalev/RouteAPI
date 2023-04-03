using AutoMapper;
using RouteAPI.Options;
using RouteAPI.Requests;

namespace RouteAPI.Services
{
    public class SearchTwoService : SearchServiceBase<ProviderTwoSearchRequest, ProviderTwoSearchResponse>
    {
        public SearchTwoService(IMapper mapper, Providers providers) : base(mapper, providers)
        {
        }

        protected override string providerName => "Provider2";
    }
}

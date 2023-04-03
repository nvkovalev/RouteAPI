using AutoMapper;
using RouteAPI.Options;
using RouteAPI.Requests;

namespace RouteAPI.Services
{
    public class SearchOneService : SearchServiceBase<ProviderOneSearchRequest, ProviderOneSearchResponse>
    {
        public SearchOneService(IMapper mapper, Providers providers) : base(mapper, providers)
        {
        }

        protected override string providerName => "Provider1";
    }
}

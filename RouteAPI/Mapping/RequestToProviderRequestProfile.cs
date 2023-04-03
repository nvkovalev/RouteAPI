using AutoMapper;
using RouteAPI.Requests;

namespace RouteAPI.Mapping
{
    public class RequestToProviderRequestProfile : Profile
    {
        public RequestToProviderRequestProfile()
        {
            CreateMap<SearchRequest, SearchRequestMain>();

            CreateMap<SearchRequest, ProviderOneSearchRequest>()
                .ForMember(x => x.DateFrom, e => e.MapFrom(m => m.OriginDateTime))
                .ForMember(x => x.DateTo, e => e.MapFrom(m => m.Filters != null ? m.Filters.DestinationDateTime : null))
                .ForMember(x => x.From, e => e.MapFrom(m => m.Origin))
                .ForMember(x => x.To, e => e.MapFrom(m => m.Destination))
                .ForMember(x => x.MaxPrice, e => e.MapFrom(m => m.Filters != null ? m.Filters.MaxPrice : null));

            CreateMap<SearchRequest, ProviderTwoSearchRequest>()
                .ForMember(x => x.DepartureDate, e => e.MapFrom(m => m.OriginDateTime))
                .ForMember(x => x.Departure, e => e.MapFrom(m => m.Origin))
                .ForMember(x => x.Arrival, e => e.MapFrom(m => m.Destination))
                .ForMember(x => x.MinTimeLimit, e => e.MapFrom(m => m.Filters != null ? m.Filters.MinTimeLimit : null));
        }
    }
}

using AutoMapper;
using RouteAPI.Requests;
using RouteAPI.Responses;

using Route = RouteAPI.Responses.Route;

namespace RouteAPI.Mapping
{
    public class ProviderResponseToResponseProfile : Profile
    {
        public ProviderResponseToResponseProfile()
        {

            #region Routes
            CreateMap<ProviderOneRoute, Route>()
                .ForMember(x => x.Id, e => e.MapFrom(m => Guid.NewGuid()))

                .ForMember(x => x.Origin, e => e.MapFrom(m => m.From))
                .ForMember(x => x.Destination, e => e.MapFrom(m => m.To))

                .ForMember(x => x.OriginDateTime, e => e.MapFrom(m => m.DateFrom))
                .ForMember(x => x.DestinationDateTime, e => e.MapFrom(m => m.DateTo))

                .ForMember(x => x.Price, e => e.MapFrom(m => m.Price))
                .ForMember(x => x.TimeLimit, e => e.MapFrom(m => m.TimeLimit));

            CreateMap<ProviderTwoRoute, Route>()
                .ForMember(x => x.Id, e => e.MapFrom(m => Guid.NewGuid()))

                .ForMember(x => x.Origin, e => e.MapFrom(m => m.Departure.Point))
                .ForMember(x => x.Destination, e => e.MapFrom(m => m.Arrival.Point))

                .ForMember(x => x.OriginDateTime, e => e.MapFrom(m => m.Departure.Date))
                .ForMember(x => x.DestinationDateTime, e => e.MapFrom(m => m.Arrival.Date))

                .ForMember(x => x.Price, e => e.MapFrom(m => m.Price))
                .ForMember(x => x.TimeLimit, e => e.MapFrom(m => m.TimeLimit));
            #endregion


            CreateMap<ProviderOneSearchResponse, SearchResponse>()
                .ForMember(x => x.Routes, e => e.MapFrom(m => m.Routes))

                .ForMember(x => x.MaxMinutesRoute, e => e.MapFrom(m => m.Routes.Max(x => (x.DateTo - x.DateFrom).TotalMinutes)))
                .ForMember(x => x.MinMinutesRoute, e => e.MapFrom(m => m.Routes.Min(x => (x.DateTo - x.DateFrom).TotalMinutes)))

                .ForMember(x => x.MaxPrice, e => e.MapFrom(m => m.Routes.Max(x => x.Price)))
                .ForMember(x => x.MinPrice, e => e.MapFrom(m => m.Routes.Min(x => x.Price)));


            CreateMap<ProviderTwoSearchResponse, SearchResponse>()
                .ForMember(x => x.Routes, e => e.MapFrom(m => m.Routes))

                .ForMember(x => x.MaxMinutesRoute, e => e.MapFrom(m => m.Routes.Max(x => (x.Arrival.Date - x.Departure.Date).TotalMinutes)))
                .ForMember(x => x.MinMinutesRoute, e => e.MapFrom(m => m.Routes.Min(x => (x.Arrival.Date - x.Departure.Date).TotalMinutes)))

                .ForMember(x => x.MaxPrice, e => e.MapFrom(m => m.Routes.Max(x => x.Price)))
                .ForMember(x => x.MinPrice, e => e.MapFrom(m => m.Routes.Min(x => x.Price)));
        }
    }
}

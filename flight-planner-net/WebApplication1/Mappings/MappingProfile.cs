using AutoMapper;
using FlightPlanner.Core.Models;
using WebApplication1.models;

namespace WebApplication1.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<FlightRequest, Flight>();
            CreateMap<AirportRequest, Airport>()
                .ForMember(airport => airport.AirportCode,
                option => option.MapFrom(request => request.Airport))
                .ForMember(airport => airport.Id,
                options => options.Ignore());
            CreateMap<Flight, FlightResponse>();
            CreateMap<Airport, AirportResponse>()
                .ForMember(response => response.Airport,
                option => option.MapFrom(request => request.AirportCode));
        }
    }
}

using AutoMapper;
using JetStreamData.FlightsService.Domain.Entities;
using JetStreamData.FlightsService.Presentation.ViewModel;

namespace JetStreamData.FlightsService.Application.Mappers;

public sealed class FlightServiceMappingProfile : Profile
{
    public FlightServiceMappingProfile()
    {
        CreateMap<Airline, AirlineViewModel>();

        CreateMap<Airport, AirportViewModel>();

        CreateMap<Flight, FlightViewModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name));

        CreateMap<Schedule, ScheduleViewModel>();

        CreateMap<FlightInformation, FlightInformationViewModel>()
            .ForMember(dest => dest.Flight, opt => opt.MapFrom(src => src.Flight))
            .ForMember(dest => dest.DepartureAirport, opt => opt.MapFrom(src => src.DepartureAirport))
            .ForMember(dest => dest.ArrivalAirport, opt => opt.MapFrom(src => src.ArrivalAirport))
            .ForMember(dest => dest.Airline, opt => opt.MapFrom(src => src.Airline))
            .ForMember(dest => dest.DepartureSchedule, opt => opt.MapFrom(src => src.DepartureSchedule))
            .ForMember(dest => dest.ArrivalSchedule, opt => opt.MapFrom(src => src.ArrivalSchedule));
    }
}

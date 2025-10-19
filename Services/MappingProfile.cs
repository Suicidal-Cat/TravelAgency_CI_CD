using AutoMapper;
using Models.Dtos;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Destination,DestinationDto>().ReverseMap();
            CreateMap<Trip, AddTripDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Trip, TripDto>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Destination, DestinationDto>().ReverseMap();
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User != null ? src.User.Username : string.Empty))
                .ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<Booking, AddBookingDto>().ReverseMap();
            CreateMap<Booking, BookingDetailsDto>()
                .ForMember(dest => dest.TripDate, opt => opt.MapFrom(src => (src.Trip.StartDate.Date + " " + src.Trip.EndDate.Date) ?? ""))
                .ForMember(dest => dest.TripDescription, opt => opt.MapFrom(src => src.Trip.Description ?? ""))
                .ReverseMap();
        }
    }
}

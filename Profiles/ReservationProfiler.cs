using AutoMapper;
using RestaurantReservationDb.domain;
using RetaurantReservationAPI.Models;

namespace RetaurantReservationAPI.Profiles
{
    public class ReservationProfiler : Profile
    {
        public ReservationProfiler() {
            CreateMap<Reservation, Models.ReservationDto>();
            CreateMap<ReservationDto, Reservation>();
        }
    }
}

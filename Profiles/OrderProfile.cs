using RestaurantReservationDb.domain;
using RetaurantReservationAPI.Models;
using AutoMapper;

namespace RetaurantReservationAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, Models.OrderDto>();
            CreateMap<OrderDto, Order>();
        }
    }
}

using RestaurantReservationDb.domain;
using RetaurantReservationAPI.Models;
using AutoMapper;

namespace RetaurantReservationAPI.Profiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem, Models.MenuItemDto>();
            CreateMap<MenuItemDto, MenuItem>();

        }
    }
}

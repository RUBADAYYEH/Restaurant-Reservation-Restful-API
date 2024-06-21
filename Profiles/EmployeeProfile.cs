using AutoMapper;
using RestaurantReservationDb.domain;
using RetaurantReservationAPI.Models;

namespace RetaurantReservationAPI.Profiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile() {
            CreateMap<Employee, Models.EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
  
        }
    }
}

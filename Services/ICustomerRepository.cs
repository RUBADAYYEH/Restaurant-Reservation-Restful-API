using RestaurantReservationDb.domain;

namespace RetaurantReservationAPI.Services
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        
    }
}

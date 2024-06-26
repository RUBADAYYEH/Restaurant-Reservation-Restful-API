using RestaurantReservationDb.domain;

namespace RetaurantReservationAPI.Services
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsByCustomerIdAsync(int customerid);
        Task<IEnumerable<Order>> GetOrdersByReservationIdAsync(int reservationId);
        Task<IEnumerable<MenuItem>> GetOrderItemsByReservationIdAsync(int reservationId);

        Task<bool> IsAvailableIdAsync(int id);

    }
}

using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservationDb.domain;

namespace RetaurantReservationAPI.Services
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public ReservationRepository(RestaurantReservationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<MenuItem>> GetOrderItemsByReservationIdAsync(int reservationId)
        {
            return await _context.Orders
            .Include(o => o.OrderItems)
             .ThenInclude(oi => oi.MenuItem)
              .Where(o => o.ReservationId == reservationId)
                .SelectMany(o => o.OrderItems.Select(oi => oi.MenuItem))
                 .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByReservationIdAsync(int reservationId)
        {
            return await _context.Orders.Where(r => r.ReservationId == reservationId).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByCustomerIdAsync(int customerid)
        {
            return await _context.Reservations.Where(r => r.CustomerId==customerid).ToListAsync();
        }

        public  async Task<bool> IsAvailableIdAsync(int id)
        {
            return await _context.Reservations.AnyAsync(e => e.ReservationId == id);

        }

       
    }
}

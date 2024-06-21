using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservationDb.domain;

namespace RetaurantReservationAPI.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RestaurantReservationDbContext _context;
        public EmployeeRepository(RestaurantReservationDbContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public RestaurantReservationDbContext Context { get; }

        public async Task AddEmployeeForRestaurant(int restaurantId, Employee employee)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);
            if (restaurant != null && employee!=null)
            {
                restaurant.Employees.Add(employee);
            }
            else
            {
                Console.WriteLine("Something is null her");
            }
        }

        public void DeleteEmployee(Employee employee)
        {
           _context.Employees.Remove(employee);
        }

        public async Task<float> GetAverageOrderAmountForEmployeeWithEmployeeId(int employeeId)
        {
             return await _context.Orders.Select(em => em).Where(em => em.EmployeeId == employeeId).Select(m => m.TotalAmount).AverageAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
           return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.OrderBy(e => e.EmployeeId).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetManagersAsync()
        {
             return await _context.Employees.Where(emp => emp.Position=="Manager").ToListAsync();
        }

        public async Task<bool> RestaurantExists(int restaurantId)
        {
            return  await _context.Restaurants.AnyAsync(rest => rest.RestaurantId == restaurantId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}

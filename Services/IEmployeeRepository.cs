using RestaurantReservationDb.domain;

namespace RetaurantReservationAPI.Services
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetManagersAsync();
        Task<float> GetAverageOrderAmountForEmployeeWithEmployeeId(int employeeId);
        Task<bool> RestaurantExists(int restaurantId);
        Task AddEmployeeForRestaurant(int restaurantId, Employee employee);
        Task<bool> SaveChangesAsync();
        void DeleteEmployee(Employee employeeid);
    }
}

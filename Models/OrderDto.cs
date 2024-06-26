namespace RetaurantReservationAPI.Models
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int ReservationId { get; set; }
        public float TotalAmount { get; set; }
        public int EmployeeId { get; set; }
    }
}

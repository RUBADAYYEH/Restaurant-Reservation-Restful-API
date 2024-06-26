using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RetaurantReservationAPI.Models;
using RetaurantReservationAPI.Services;

namespace RetaurantReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _repo;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet("{reservationid}/orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersForReservationId(int reservationid)
        {
            var orders = await _repo.GetOrdersByReservationIdAsync(reservationid);
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }
        [HttpGet("{reservationid}/menu-items")]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>>  GetOrderItemsByReservationId(int reservationid)
        {
            var items = await _repo.GetOrderItemsByReservationIdAsync(reservationid);
            return Ok(_mapper.Map<IEnumerable<MenuItemDto>>(items));
        }

        [HttpGet("/customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetReservationsByCustomerid(int customerId)
        {
            var reservs = await _repo.GetReservationsByCustomerIdAsync(customerId);
            return Ok(_mapper.Map<IEnumerable<ReservationDto>>(reservs));
        }


    }
}

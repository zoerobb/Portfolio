using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HW6.Models;
using HW6.Models.DTO;
using HW6.DAL.Abstract;
using HW6.Services;
using HW6.ExtensionMethods;

namespace HW6.Controllers
{
    [Route("api")]
    [ApiController]

    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IStationRepository _stationRepository;

        public OrderApiController(IOrderService orderService, IStationRepository stationRepository)
        {
            _orderService = orderService;
            _stationRepository = stationRepository;
        }

        [HttpPost("runOrderGenerator")]
        public async Task<IActionResult> RunOrderGenerator(ReceivedOrder data)
        {
            try
            {
                await _orderService.RunOrderGeneratorScript(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error running order generator script");
            }
        }

        //PUT: api/updateOrders
        [HttpPut("updateOrders")]
        public IActionResult UpdateOrders()
        {
            try
            {
                _orderService.UpdateAllOrders();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound($"Error updating orders");
            }
        }

        //GET: api/orders
        [HttpGet("orders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDTO>))]
        public ActionResult<IEnumerable<OrderDTO>> GetOrders()
        {
            try
            {
                return _orderService.GetOrders().Select(s => s.ToOrderDTO()).ToList();
            }
            catch (Exception ex)
            {
                return NotFound($"No orders found");
            }
        }

        //GET: api/orderedItems/1
        [HttpGet("orderedItems/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderedItemDTO>))]
        public ActionResult<IEnumerable<OrderedItemDTO>> GetOrderedItems(int id)
        {
            try
            {
                return _orderService.GetOrderedItems(id).Select(s => s.ToOrderedItemDTO()).ToList();
            }
            catch (Exception ex)
            {
                return NotFound($"No ordered items found for order with ID {id}");
            }
        }

        //GET: api/stations
        [HttpGet("stations")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StationDTO>))]
        public ActionResult<IEnumerable<StationDTO>> GetStations()
        {
            try
            {
                return _stationRepository.GetAll().Select(s => s.ToStationDTO()).ToList();
            }
            catch (Exception ex)
            {
                return NotFound($"No stations found");
            }
        }

        //GET: api/station/1
        [HttpGet("station/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public ActionResult<string> GetStation(int id)
        {
            try
            {
                return _stationRepository.GetStationName(id);
            }
            catch (Exception ex)
            {
                return NotFound($"No station found with ID {id}");
            }
        }

        [HttpGet("stationItems/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderedItemDTO>))]
        public ActionResult<IEnumerable<OrderedItemDTO>> GetOrderedItemsForAStation(int id)
        {
            try
            {
                return _orderService.GetOrderedItemsForAStation(id).Select(s => s.ToOrderedItemDTO()).ToList();
            }
            catch (Exception ex)
            {
                return NotFound($"No ordered items found for station with ID {id}");
            }
        }

        //PUT: api/completeOrderItem/1
        [HttpPut("completeOrderItem/{id}")]
        public IActionResult CompleteOrderItem(int id)
        {
            try
            {
                _orderService.CompleteOrderItem(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error completing order item");
            }
        }
     }
}

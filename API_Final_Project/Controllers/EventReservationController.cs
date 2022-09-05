using API_Final_Project.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventReservationController : Controller
    {
        public IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        [HttpGet("/eventReservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<EventReservation>> ConsultarReservas()
        {
            return Ok(_eventReservationService.ConsultarReservas()); //=> métodos da interface
        }
    }
}

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


        [HttpPost("/eventReservation/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EventReservation> CriarReserva(EventReservation eventReservation)
        {
            if (!_eventReservationService.CriarReserva(eventReservation))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(CriarReserva), eventReservation);
        }


        [HttpPut("/eventReservation/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EditarReserva(long id, EventReservation eventReservation)
        {
            if (!_eventReservationService.EditarReserva(id, eventReservation))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("/eventReservation/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> ExcluirReserva(long Id)
        {
            if (!_eventReservationService.ExcluirReserva(Id))
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}

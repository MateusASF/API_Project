using API_Final_Project.Core.Interfaces;
using API_Final_Project.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EventReservationController : Controller
    {
        public IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        [HttpGet("/eventReservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public ActionResult<List<EventReservation>> ConsultarReservas()
        {
            return Ok(_eventReservationService.ConsultarReservas()); 
        }

        [HttpGet("/eventReservation/id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public ActionResult<List<EventReservation>> ConsultarReservasId(long id)
        {
            return Ok(_eventReservationService.ConsultarReservasId(id));
        }

        [HttpGet("/eventReservation/search/{personName}/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public ActionResult<List<Object>> ConsultarEventosPersonNameTitle(string personName, string title)
        {
            return Ok(_eventReservationService.ConsultarEventosPersonNameTitle(personName, title));
        }


        [HttpPost("/eventReservation/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin, cliente")]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_RegistroExistente_Reservation))]
        [Authorize(Roles = "admin")]
        public IActionResult EditarReserva(long id, EventReservation eventReservation)
        {
            if (!_eventReservationService.EditarReserva(id, eventReservation))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

        [HttpDelete("/eventReservation/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_RegistroExistente_Reservation))]
        [Authorize(Roles = "admin")]
        public ActionResult<List<CityEvent>> ExcluirReserva(long Id)
        {
            if (!_eventReservationService.ExcluirReserva(Id))
            {
                new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

    }
}

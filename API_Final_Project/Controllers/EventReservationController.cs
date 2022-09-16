using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;
using APIEvents.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIEvents.Controllers
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

        [HttpGet("/eventReservation/general")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = "admin, cliente")]
        public async Task<ActionResult<List<EventReservation>>> ConsultarReservasAsync()
        {
            return Ok(await _eventReservationService.ConsultarReservasAsync());
        }

        [HttpGet("/eventReservation/id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = "admin, cliente")]
        public async Task<ActionResult<List<EventReservation>>> ConsultarReservasIdAsync(long id)
        {
            return Ok(await _eventReservationService.ConsultarReservasIdAsync(id));
        }

        [HttpGet("/eventReservation/search/{personName}/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = "admin, cliente")]
        public async Task <ActionResult<List<object>>> ConsultarEventosPersonNameTitleAsync(string personName, string title)
        {
            return Ok(await _eventReservationService.ConsultarEventosPersonNameTitleAsync(personName, title));
        }


        [HttpPost("/eventReservation/insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(LogActionFilter_BlockReservation))]
        //[Authorize(Roles = "admin, cliente")]
        public async Task<ActionResult<EventReservation>> CriarReservaAsynck(EventReservation eventReservation)
        {
            if (!await _eventReservationService.CriarReservaAsync(eventReservation))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(CriarReservaAsynck), eventReservation);
        }


        [HttpPut("/eventReservation/{id}/{quantidade}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_RegistroExistente_Reservation))]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> EditarReservaAsync(long id, long quantidade)
        {
            if (!await _eventReservationService.EditarReservaAsync(id, quantidade))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

        [HttpDelete("/eventReservation/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_RegistroExistente_Reservation))]
        //[Authorize(Roles = "admin")]
        public async Task <ActionResult<List<CityEvent>>> ExcluirReservaAsync(long Id)
        {
            if (!await _eventReservationService.ExcluirReservaAsync(Id))
            {
                new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

    }
}

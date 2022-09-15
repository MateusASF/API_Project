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
    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        #region GET'S
        [HttpGet("/cityEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin")]
        public ActionResult<List<CityEvent>> ConsultarEventos()
        {
            return Ok(_cityEventService.ConsultarEventos()); //=> métodos da interface
        }


        [HttpGet("/cityEvent/nameEvent/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public ActionResult<List<CityEvent>> ConsultarEventosNome(string nome)
        {
            return Ok(_cityEventService.ConsultarEventosNome(nome));
        }

        [HttpGet("/cityEvent/{Local}/{Data}/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public ActionResult<List<CityEvent>> ConsultarEventosLocalData(string Local, DateTime Data)
        {
            return Ok(_cityEventService.ConsultarEventosLocalData(Local, Data));
        }

        [HttpGet("/cityEvent/{min}/{max}/{Data}/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public ActionResult<List<CityEvent>> ConsultarEventosPrecoData([FromQuery] decimal min, [FromQuery] decimal max, [FromQuery] DateTime Data)
        {
            return Ok(_cityEventService.ConsultarEventosPrecoData(min, max, Data));
        }
        #endregion


        [HttpPost("/cityEvent/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult<CityEvent> CriarEvento(CityEvent cityEvent)
        {
            if (!_cityEventService.CriarEvento(cityEvent))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(CriarEvento), cityEvent);
        }


        [HttpPut("/cityEvent/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_RegistroExistente_City))]
        [Authorize(Roles = "admin")]
        public IActionResult EditarEvento(long id, CityEvent cityEvent)
        {
            if (!_cityEventService.EditarEvento(id, cityEvent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }


        [HttpDelete("/cityEvent/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_RegistroExistente_City))]
        [ServiceFilter(typeof(RemoveEvent))]
        [Authorize(Roles = "admin")]
        public ActionResult<List<CityEvent>> ExcluirEvento(long Id)
        {
            if (!_cityEventService.ExcluirEvento(Id))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
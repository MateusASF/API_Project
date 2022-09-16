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
        [HttpGet("/cityEvent/general")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin")]
        public async Task <ActionResult<List<CityEvent>>> ConsultarEventosAsync()
        {
            return Ok(await _cityEventService.ConsultarEventosAsync()); //=> métodos da interface
        }


        [HttpGet("/cityEvent/nameEvent/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public async Task <ActionResult<List<CityEvent>>> ConsultarEventosNomeAsync(string nome)
        {
            return Ok(await _cityEventService.ConsultarEventosNomeAsync(nome));
        }

        [HttpGet("/cityEvent/{Local}/{Data}/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public async Task <ActionResult<List<CityEvent>>> ConsultarEventosLocalDataAsync(string Local, DateTime Data)
        {
            return Ok(await _cityEventService.ConsultarEventosLocalDataAsync(Local, Data));
        }

        [HttpGet("/cityEvent/{min}/{max}/{Data}/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin, cliente")]
        public async Task<ActionResult<List<CityEvent>>> ConsultarEventosPrecoDataAsync([FromQuery] decimal min, [FromQuery] decimal max, [FromQuery] DateTime Data)
        {
            return Ok(await _cityEventService.ConsultarEventosPrecoDataAsync(min, max, Data));
        }
        #endregion


        [HttpPost("/cityEvent/insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]
        public async Task <ActionResult<CityEvent>> CriarEventoAsync(CityEvent cityEvent)
        {
            if (!await _cityEventService.CriarEventoAsync(cityEvent))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(CriarEventoAsync), cityEvent);
        }


        [HttpPut("/cityEvent/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_RegistroExistente_City))]
        [Authorize(Roles = "admin")]
        public async Task <IActionResult> EditarEventoAsync(long id, CityEvent cityEvent)
        {
            if (!await _cityEventService.EditarEventoAsync(id, cityEvent))
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
        public async Task <ActionResult<List<CityEvent>>> ExcluirEventoAsync(long Id)
        {
            if (!await _cityEventService.ExcluirEventoAsync(Id))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
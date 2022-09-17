using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;
using APIEvents.Filters;
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
        //[Authorize(Roles = "admin")]
        public async Task <ActionResult<List<CityEvent>>> ConsultarEventosAsync()
        {
            var list = await _cityEventService.ConsultarEventosAsync();
            if (list.Count == 0)
            {
                return Ok("Sem Shows válidos agendados, tente cadastrar um show");
            }
            return Ok(list);
        }


        [HttpGet("/cityEvent/nameEvent/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = "admin, cliente")]
        public async Task <ActionResult<List<CityEvent>>> ConsultarEventosNomeAsync(string nome)
        {
            var list = await _cityEventService.ConsultarEventosNomeAsync(nome);
            if (list.Count == 0)
            {
                return Ok("Sem Shows marcados com o nome dado, tente fazer uma nova consulta");
            }
            return Ok(list);
        }

        [HttpGet("/cityEvent/{Local}/{Data}/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = "admin, cliente")]
        public async Task <ActionResult<List<CityEvent>>> ConsultarEventosLocalDataAsync(string Local, DateTime Data)
        {
            var list = await _cityEventService.ConsultarEventosLocalDataAsync(Local, Data);
            if (list.Count == 0)
            {
                return Ok("Sem Shows marcados com os parametros passados, tente fazer uma nova consulta");
            }
            return Ok(list);
        }

        [HttpGet("/cityEvent/{min}/{max}/{Data}/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = "admin, cliente")]
        public async Task<ActionResult<List<CityEvent>>> ConsultarEventosPrecoDataAsync([FromQuery] decimal min, [FromQuery] decimal max, [FromQuery] DateTime Data)
        {
            var list = await _cityEventService.ConsultarEventosPrecoDataAsync(min, max, Data);
            if (list.Count == 0)
            {
                return Ok("Sem Shows marcados com os parametros passados, tente fazer uma nova consulta");
            }
            return Ok(list);
        }
        #endregion


        [HttpPost("/cityEvent/insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "admin")]
        public async Task <ActionResult<CityEvent>> CriarEventoAsynck(CityEvent cityEvent)
        {
            if (!await _cityEventService.CriarEventoAsync(cityEvent))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(CriarEventoAsynck), cityEvent);
        }


        [HttpPut("/cityEvent/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_RegistroExistente_City))]
        //[Authorize(Roles = "admin")]
        public async Task <IActionResult> EditarEventoAsync(long id, CityEvent cityEvent)
        {
            if (!await _cityEventService.EditarEventoAsync(id, cityEvent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }


        [HttpDelete("/cityEvent/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_RegistroExistente_City))]
        [ServiceFilter(typeof(LogActionFilter_RemoveEvent))]
        //[Authorize(Roles = "admin")]
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
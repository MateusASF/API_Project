using API_Final_Project.Core.Interfaces;
using API_Final_Project.Filters;
using Microsoft.AspNetCore.Mvc;

namespace API_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")] 
    [Produces("application/json")]
    [TypeFilter(typeof(LogActionFilter_AdmUser_City))] // -> para qualquer recurso da tabela city Event,
                                                       // o Header deverá ter um campo "User" com o valor "adm"
    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("/cityEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CityEvent>> ConsultarEventos()
        {
            return Ok(_cityEventService.ConsultarEventos()); //=> métodos da interface
        }

        [HttpPost("/cityEvent/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CityEvent> CriarEvento(CityEvent cityEvent)
        {
            if (!_cityEventService.CriarEvento(cityEvent))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(CriarEvento), cityEvent);
        }


        [HttpPut("/cityEvent/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_RegistroExistente_City))]
        public IActionResult EditarEvento(long id, CityEvent cityEvent)
        {
            if (!_cityEventService.EditarEvento(id, cityEvent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }


        [HttpDelete("/cityEvent/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilter_RegistroExistente_City))]
        public ActionResult<List<CityEvent>> ExcluirEvento (long Id)
        {
            if (!_cityEventService.ExcluirEvento(Id))
            {
                new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
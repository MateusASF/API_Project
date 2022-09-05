using API_Final_Project.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult EditarEvento(long id, CityEvent cityEvent)
        {
            if (!_cityEventService.EditarEvento(id, cityEvent))
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpDelete("/cityEvent/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityEvent>> ExcluirEvento (long Id)
        {
            if (!_cityEventService.ExcluirEvento(Id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
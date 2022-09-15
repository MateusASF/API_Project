using APIEvents.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
//LogActionFilter_PostExistente_
namespace APIEvents.Filters
{
    public class LogActionFilter_RegistroExistente_City : ActionFilterAttribute
    {
        ICityEventService _cityEventService;

        public LogActionFilter_RegistroExistente_City(ICityEventService clienteService)
        {
            _cityEventService = clienteService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idEvent = (long)context.ActionArguments["id"];

            if (_cityEventService.ConsultarEventosid(idEvent) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }

    }
}

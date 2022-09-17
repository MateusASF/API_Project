using APIEvents.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvents.Filters
{
    public class LogActionFilter_RegistroExistente_City : IAsyncActionFilter
    {
        readonly ICityEventService _cityEventService;

        public LogActionFilter_RegistroExistente_City(ICityEventService clienteService)
        {
            _cityEventService = clienteService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            long idEvent = (long)context.ActionArguments["id"];
            var problem = new ProblemDetails();
            if (_cityEventService.ConsultarEventosidAsync(idEvent) == null)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problem.Status = 400;
                problem.Title = "Evento Inexistente";
                problem.Detail = "Este Evento não existe";
                problem.Type = GetType().Name;
                context.Result = new ObjectResult(problem);
            }
            await next();
        }

    }
}

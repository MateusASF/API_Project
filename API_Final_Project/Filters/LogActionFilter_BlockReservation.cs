using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvents.Filters
{
    public class LogActionFilter_BlockReservation : IAsyncActionFilter
    {
        readonly ICityEventService _cityEventService;

        public LogActionFilter_BlockReservation(ICityEventService clienteService)
        {
            _cityEventService = clienteService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var evento = context.ActionArguments["eventReservation"] as EventReservation;
            var list = _cityEventService.ConsultarEventosAsync().Result.ToList();
            var problem = new ProblemDetails();
            if (!list.Any(x => x.IdEvent == evento.IdEvent))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                problem.Status = 409;
                problem.Title = "Tentativa de reserva em evento inativo";
                problem.Detail = "Este Evento não esta habilitado";
                problem.Type = GetType().Name;
                context.Result = new ObjectResult(problem);
            }
            await next();
        }
    }
}

using APIEvents.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvents.Filters
{
    public class LogActionFilter_RegistroExistente_Reservation : IAsyncActionFilter
    {
        readonly IEventReservationService _eventReservationService;

        public LogActionFilter_RegistroExistente_Reservation(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            long idReservation = (long)context.ActionArguments["id"];
            var problem = new ProblemDetails();
            if (_eventReservationService.ConsultarReservasIdAsync(idReservation) == null)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problem.Status = 400;
                problem.Title = "Reserva Inexistente";
                problem.Detail = "Esta Reserva não existe";
                problem.Type = GetType().Name;
                context.Result = new ObjectResult(problem);
            }
            await next();
        }

    }
}

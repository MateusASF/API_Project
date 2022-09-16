using APIEvents.Controllers;
using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvents.Filters
{
    public class LogActionFilter_RegistroExistente_Reservation : ActionFilterAttribute
    {
        readonly IEventReservationService _eventReservationService;

        public LogActionFilter_RegistroExistente_Reservation(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
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
        }

    }
}

using APIEvents.Core.Interfaces;
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

            if (_eventReservationService.ConsultarReservasIdAsync(idReservation) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }

    }
}

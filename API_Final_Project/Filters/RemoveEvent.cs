using APIEvents.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace APIEvents.Filters
{
    public class RemoveEvent : ActionFilterAttribute
    {
        readonly IEventReservationService _eventReservationService;
        readonly ICityEventService _cityEventService;

        public RemoveEvent(IEventReservationService eventReservationService, ICityEventService cityEventService)
        {
            _eventReservationService = eventReservationService;
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            long idEvent = (long)context.ActionArguments["id"];

            if (_eventReservationService.RemoveEventAsync(idEvent).Result.Count > 0)
            {
                _cityEventService.AlterStatusAsync(idEvent);
                var result = new ObjectResult(new { erro = "O evento possui reserva e foi inativado" })
                {
                    StatusCode = 204
                };
                context.Result = result;
            }
            else
            {
                return;
            }
        }
    }
}

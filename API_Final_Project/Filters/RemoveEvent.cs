using API_Final_Project.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIEvents.Filters
{
    public class RemoveEvent : ActionFilterAttribute
    {
        IEventReservationService _eventReservationService;
        ICityEventService _cityEventService;

        public RemoveEvent(IEventReservationService eventReservationService, ICityEventService cityEventService)
        {
            _eventReservationService = eventReservationService;
            _cityEventService = cityEventService; 
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idEvent = (long)context.ActionArguments["id"];


            if (_eventReservationService.RemoveEvent(idEvent).Count > 0)
            {
                _cityEventService.Upper(idEvent);
                var result = new ObjectResult(new { erro = "O evento possui reserva e foi inativado" });
                result.StatusCode = 204;
                context.Result = result;
            }
            else
            {
                return;
            }
        }
    }
}

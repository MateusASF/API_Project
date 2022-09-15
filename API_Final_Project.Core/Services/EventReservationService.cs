using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;

namespace APIEvents.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;

        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

        public List<EventReservation> ConsultarReservas()
        {
            return _eventReservationRepository.ConsultarReservas();
        }

        public List<object> ConsultarEventosPersonNameTitle(string personName, string title)
        {
            return _eventReservationRepository.ConsultarEventosPersonNameTitle(personName, title);
        }

        public EventReservation ConsultarReservasId(long idReservation)
        {
            return _eventReservationRepository.ConsultarReservasId(idReservation);
        }

        public bool CriarReserva(EventReservation eventReservation)
        {
            return _eventReservationRepository.CriarReserva(eventReservation);
        }

        public bool EditarReserva(long Id, long quantidade)
        {
            return _eventReservationRepository.EditarReserva(Id, quantidade);
        }

        public bool ExcluirReserva(long Id)
        {
            return _eventReservationRepository.ExcluirReserva(Id);
        }

        public List<EventReservation> RemoveEvent(long Id)
        {
            return _eventReservationRepository.RemoveEvent(Id);
        }
    }
}

using API_Final_Project.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Final_Project.Core.Service
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

        public List<Object> ConsultarEventosPersonNameTitle(string personName, string title)
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

        public bool EditarReserva(long Id, EventReservation eventReservation)
        {
            return _eventReservationRepository.EditarReserva(Id, eventReservation);
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

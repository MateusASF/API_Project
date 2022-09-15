using APIEvents.Core.Models;

namespace APIEvents.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        List<EventReservation> ConsultarReservas();

        List<object> ConsultarEventosPersonNameTitle(string personName, string title);

        EventReservation ConsultarReservasId(long idReservation);

        bool CriarReserva(EventReservation eventReservation);

        bool EditarReserva(long Id, long quantidade);

        bool ExcluirReserva(long Id);

        List<EventReservation> RemoveEvent(long Id);

    }
}

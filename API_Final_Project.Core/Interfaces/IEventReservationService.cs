using APIEvents.Core.Models;

namespace APIEvents.Core.Interfaces
{
    public interface IEventReservationService
    {
        Task <List<EventReservation>> ConsultarReservasAsync();

        Task<List<object>> ConsultarEventosPersonNameTitleAsync(string personName, string title);

        Task<EventReservation> ConsultarReservasIdAsync(long idReservation);

        Task<bool> CriarReservaAsync(EventReservation eventReservation);

        Task<bool> EditarReservaAsync(long Id, long quantidade);

        Task <bool> ExcluirReservaAsync(long Id);

        Task<List<EventReservation>> RemoveEventAsync(long Id);
    }
}

using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace APIEvents.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;

        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

        public async Task<List<EventReservation>> ConsultarReservasAsync()
        {
            return await _eventReservationRepository.ConsultarReservasAsync();
        }

        public async Task<List<object>> ConsultarEventosPersonNameTitleAsync(string personName, string title)
        {
            return await _eventReservationRepository.ConsultarEventosPersonNameTitleAsync(personName, title);
        }

        public async Task<EventReservation> ConsultarReservasIdAsync(long idReservation)
        {
            return await _eventReservationRepository.ConsultarReservasIdAsync(idReservation);
        }

        public async Task<bool> CriarReservaAsync(EventReservation eventReservation)
        {
            return await _eventReservationRepository.CriarReservaAsync(eventReservation);
        }

        public async Task<bool> EditarReservaAsync(long Id, long quantidade)
        {
            return await _eventReservationRepository.EditarReservaAsync(Id, quantidade);
        }

        public async Task<bool> ExcluirReservaAsync(long Id)
        {
            return await _eventReservationRepository.ExcluirReservaAsync(Id);
        }

        public async Task<List<EventReservation>> RemoveEventAsync(long Id)
        {
            return await _eventReservationRepository.RemoveEventAsync(Id);
        }
    }
}

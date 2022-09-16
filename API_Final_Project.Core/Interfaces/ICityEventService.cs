﻿using APIEvents.Core.Models;

namespace APIEvents.Core.Interfaces
{
    public interface ICityEventService
    {
        Task<List<CityEvent>> ConsultarEventosAsync();

        Task<List<CityEvent>> ConsultarEventosNomeAsync(string nome);

        Task<List<CityEvent>> ConsultarEventosLocalDataAsync(string Local, DateTime Data);

        Task<List<CityEvent>> ConsultarEventosPrecoDataAsync(decimal min, decimal max, DateTime Data);

        Task<CityEvent> ConsultarEventosidAsync(long idEvent);

        Task<bool> CriarEventoAsync(CityEvent cityEvent);

        Task<bool> EditarEventoAsync(long Id, CityEvent cityEvent);

        Task<bool> ExcluirEventoAsync(long Id);

        Task<bool> AlterStatusAsync(long Id);
    }
}

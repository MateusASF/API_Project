using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;

namespace APIEvents.Core.Services
{
    public class CityEventService : ICityEventService
    {
        public ICityEventRepository _cityEventRepository;

        public CityEventService(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        public async Task<List<CityEvent>> ConsultarEventosAsync()
        {
            return await _cityEventRepository.ConsultarEventosAsync();
        }

        public async Task<List<CityEvent>> ConsultarEventosNomeAsync(string nome)
        {
            return await _cityEventRepository.ConsultarEventosNomeAsync(nome);
        }
        public async Task<List<CityEvent>> ConsultarEventosLocalDataAsync(string Local, DateTime Data)
        {
            return await _cityEventRepository.ConsultarEventosLocalDataAsync(Local, Data);
        }

        public async Task<CityEvent> ConsultarEventosidAsync(long idEvent)
        {
            return await _cityEventRepository.ConsultarEventosidAsync(idEvent);
        }

        public async Task<bool> CriarEventoAsync(CityEvent cityEvent)
        {
            return await _cityEventRepository.CriarEventoAsync(cityEvent);
        }

        public async Task<bool> EditarEventoAsync(long id, CityEvent cityEvent)
        {
            return await _cityEventRepository.EditarEventoAsync(id, cityEvent);
        }

        public async Task<List<CityEvent>> ConsultarEventosPrecoDataAsync(decimal min, decimal max, DateTime Data)
        {
            return await _cityEventRepository.ConsultarEventosPrecoDataAsync(min, max, Data);
        }

        public async Task <bool> ExcluirEventoAsync(long Id)
        {
            return await _cityEventRepository.ExcluirEventoAsync(Id); ;
        }


        public async Task<bool> AlterStatusAsync(long Id)
        {
            return await _cityEventRepository.AlterStatusAsync(Id);
        }
    }
}

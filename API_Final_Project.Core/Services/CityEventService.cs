using API_Final_Project.Core.Interfaces;

namespace API_Final_Project.Core.Service
{
    public class CityEventService : ICityEventService
    {
        public ICityEventRepository _cityEventRepository;

        public CityEventService(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        public List<CityEvent> ConsultarEventos()
        {
            return _cityEventRepository.ConsultarEventos();
        }
    }
}

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

        public List<CityEvent> ConsultarEventosNome(string nome)
        {
            return _cityEventRepository.ConsultarEventosNome(nome);
        } 
        public List<CityEvent> ConsultarEventosLocalData(string Local, DateTime Data)
        {
            return _cityEventRepository.ConsultarEventosLocalData(Local, Data);
        }

        public CityEvent ConsultarEventosid(long idEvent)
        {
            return _cityEventRepository.ConsultarEventosid(idEvent);
        }

        public bool CriarEvento(CityEvent cityEvent)
        {
            return _cityEventRepository.CriarEvento(cityEvent);
        }

        public bool EditarEvento(long Id, CityEvent cityEvent)
        {
            return _cityEventRepository.EditarEvento(Id, cityEvent);
        }        
        public List<CityEvent> ConsultarEventosPrecoData(decimal min, decimal max, DateTime Data)
        {
            return _cityEventRepository.ConsultarEventosPrecoData(min, max, Data);
        }

        public bool ExcluirEvento(long Id)
        {
            return _cityEventRepository.ExcluirEvento(Id);
        }       
        

        public bool Upper(long Id)
        {
            return _cityEventRepository.Upper(Id);
        }
    }
}

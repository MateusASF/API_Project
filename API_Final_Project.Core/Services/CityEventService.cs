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
            bool crea;
            try
            {
                crea = _cityEventRepository.CriarEvento(cityEvent);
            }
            catch (NullReferenceException ex)
            {
                var mensagem = ex.Message;
                var caminho = ex.StackTrace;
                var teste = ex.TargetSite;
                Console.WriteLine($"Valores nulos, mensagem {mensagem}, stack trace {caminho}, {teste}");
                crea = false;
                return crea;
            }
            catch (Exception ex)
            {
                var tipoExcecao = ex.GetType().Name;
                var mensagem = ex.Message;
                var caminho = ex.StackTrace;
                Console.WriteLine($"Tipo da exceção {tipoExcecao}, mensagem {mensagem}, stack trace {caminho}");
                crea = false;
                return crea;
            }
            return crea;
        }

        public bool EditarEvento(long Id, CityEvent cityEvent)
        {
            bool edit;
            try
            {
                edit = _cityEventRepository.EditarEvento(Id, cityEvent);
            }
            catch (NullReferenceException ex)
            {
                var mensagem = ex.Message;
                var caminho = ex.StackTrace;
                var teste = ex.TargetSite;
                Console.WriteLine($"Valores nulos, mensagem {mensagem}, stack trace {caminho}, {teste}");
                edit = false;
                return edit;
            }
            catch (Exception ex)
            {
                var tipoExcecao = ex.GetType().Name;
                var mensagem = ex.Message;
                var caminho = ex.StackTrace;
                Console.WriteLine($"Tipo da exceção {tipoExcecao}, mensagem {mensagem}, stack trace {caminho}");
                edit = false;
                return edit;
            }
            return edit;
        }        

        public List<CityEvent> ConsultarEventosPrecoData(decimal min, decimal max, DateTime Data)
        {
            return _cityEventRepository.ConsultarEventosPrecoData(min, max, Data);
        }

        public bool ExcluirEvento(long Id)
        {
            bool excl;
            try
            {
                excl = _cityEventRepository.ExcluirEvento(Id);
            }
            catch (NullReferenceException ex)
            {
                var mensagem = ex.Message;
                var caminho = ex.StackTrace;
                var teste = ex.TargetSite;
                Console.WriteLine($"Valores nulos, mensagem {mensagem}, stack trace {caminho}, {teste}");
                excl = false;
                return excl;
            }
            catch (Exception ex)
            {
                var tipoExcecao = ex.GetType().Name;
                var mensagem = ex.Message;
                var caminho = ex.StackTrace;
                Console.WriteLine($"Tipo da exceção {tipoExcecao}, mensagem {mensagem}, stack trace {caminho}");
                excl = false;
                return excl;
            }
            return excl;
        }       
        

        public bool Upper(long Id)
        {
            return _cityEventRepository.Upper(Id);
        }
    }
}

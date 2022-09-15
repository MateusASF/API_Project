using APIEvents.Core.Models;

namespace APIEvents.Core.Interfaces
{
    public interface ICityEventService
    {
        List<CityEvent> ConsultarEventos();

        List<CityEvent> ConsultarEventosNome(string nome);

        List<CityEvent> ConsultarEventosLocalData(string Local, DateTime Data);
        List<CityEvent> ConsultarEventosPrecoData(decimal min, decimal max, DateTime Data);

        CityEvent ConsultarEventosid(long idEvent);

        bool CriarEvento(CityEvent cityEvent);

        bool EditarEvento(long Id, CityEvent cityEvent);

        bool ExcluirEvento(long Id);
        bool AlterStatus(long Id);
    }
}

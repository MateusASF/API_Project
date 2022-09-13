using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Final_Project.Core.Interfaces
{
    public interface ICityEventService
    {
        List<CityEvent> ConsultarEventos();

        List<CityEvent> ConsultarEventosNome(string nome);

        List<CityEvent> ConsultarEventosLocalData(string Local, DateTime Data);
        List<CityEvent> ConsultarEventosPrecoData(decimal min, decimal max, DateTime Data);

        CityEvent ConsultarEventosid (long idEvent);

        bool CriarEvento(CityEvent cityEvent);

        bool EditarEvento(long Id, CityEvent cityEvent);

        bool ExcluirEvento(long Id);
        bool Upper(long Id);
    }
}

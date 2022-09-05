using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Final_Project.Core.Interfaces
{
    public interface ICityEventRepository
    {
        List<CityEvent> ConsultarEventos();
        bool CriarEvento(CityEvent cityEvent);
        bool EditarEvento(long Id, CityEvent cityEvent);
        bool ExcluirEvento(long Id);
    }
}

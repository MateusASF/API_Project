using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Final_Project.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        List<EventReservation> ConsultarReservas();
    }
}

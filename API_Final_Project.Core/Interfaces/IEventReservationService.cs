﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Final_Project.Core.Interfaces
{
    public interface IEventReservationService
    {
        List<EventReservation> ConsultarReservas();

        EventReservation ConsultarReservasId(long idReservation);

        bool CriarReserva (EventReservation eventReservation);

        bool EditarReserva(long Id, EventReservation eventReservation);

        bool ExcluirReserva(long Id);
    }
}

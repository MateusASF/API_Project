using API_Final_Project.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Final_Project.Core.Service
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;

        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

        public List<EventReservation> ConsultarReservas()
        {
            return _eventReservationRepository.ConsultarReservas();
        }

        public List<Object> ConsultarEventosPersonNameTitle(string personName, string title)
        {
            return _eventReservationRepository.ConsultarEventosPersonNameTitle(personName, title);
        }

        public EventReservation ConsultarReservasId(long idReservation)
        {
            return _eventReservationRepository.ConsultarReservasId(idReservation);
        }

        public bool CriarReserva(EventReservation eventReservation)
        {
            bool crea;
            try
            {
                crea = _eventReservationRepository.CriarReserva(eventReservation);
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

        public bool EditarReserva(long Id, EventReservation eventReservation)
        {
            bool edit;
            try
            {
                edit = _eventReservationRepository.EditarReserva(Id, eventReservation);
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

        public bool ExcluirReserva(long Id)
        {
            bool excl;
            try
            {
                excl = _eventReservationRepository.ExcluirReserva(Id);
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

        public List<EventReservation> RemoveEvent(long Id)
        {
            return _eventReservationRepository.RemoveEvent(Id);
        }
    }
}

using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace APIEvents.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;

        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<EventReservation> ConsultarReservas()
        {
            var query = "SELECT * FROM EventReservation";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var lala = conn.Query<EventReservation>(query).ToList();
            return lala;
        }

        public EventReservation ConsultarReservasId(long IdReservation)
        {
            var query = "SELECT * FROM EventReservation WHERE IdReservation = @IdReservation";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DynamicParameters parameters = new(new { IdReservation });
            return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
        }

        public List<object> ConsultarEventosPersonNameTitle(string personName, string title)
        {
            var query = @$"SELECT * FROM EventReservation AS event
                           INNER JOIN CityEvent AS city ON
                           event.PersonName = @personName AND city.Title like ('%' + @title '%') AND event.IdEvent = city.IdEvent ";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DynamicParameters parameters = new(new { title, personName });
            return conn.Query<object>(query, parameters).ToList();
        }

        public bool CriarReserva(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES (@IdEvent, @PersonName, @Quantity)";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DynamicParameters parameters = new(new { eventReservation.IdEvent, eventReservation.PersonName, eventReservation.Quantity });
            return conn.Execute(query, parameters) == 1;
        }

        public bool EditarReserva(long IdReservation, long Quantity)
        {
            var query = @"UPDATE EventReservation SET
                          Quantity = @Quantity
                          WHERE IdReservation = @IdReservation";
            DynamicParameters parameters = new(new { IdReservation, Quantity });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

        public bool ExcluirReserva(long IdReservation)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @IdReservation";
            DynamicParameters parameters = new(new { IdReservation });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

        public List<EventReservation> RemoveEvent(long IdEvent)
        {
            var query = "select * from EventReservation where IdEvent = @IdEvent";
            DynamicParameters parameters = new(new { IdEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<EventReservation>(query, parameters).ToList();
        }


    }
}

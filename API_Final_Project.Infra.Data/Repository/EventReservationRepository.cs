using API_Final_Project.Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API_Final_Project.Infra.Data.Repository
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
            return conn.Query<EventReservation>(query).ToList();
        }

        public bool CriarReserva(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES (@IdEvent, @PersonName, @Quantity)";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DynamicParameters parameters = new(new { eventReservation.IdEvent, eventReservation.PersonName, eventReservation.Quantity });
            return conn.Execute(query, parameters) == 1;
        }

        public bool EditarReserva(long Id, EventReservation eventReservation)
        {
            var query = @"UPDATE EventReservation SET IdEvent = @IdEvent,
                          PersonName = @PersonName,
                          Quantity = @Quantity
                          WHERE IdEvent = @IdEvent";
            eventReservation.IdEvent = Id;
            DynamicParameters parameters = new(eventReservation);
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

        public bool ExcluirReserva(long Id)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @IdReservation";
            DynamicParameters parameters = new(Id);
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

    }
}

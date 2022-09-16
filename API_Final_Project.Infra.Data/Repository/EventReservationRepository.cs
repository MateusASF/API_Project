using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;

namespace APIEvents.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;

        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<EventReservation>> ConsultarReservasAsync()
        {
            var query = "SELECT * FROM EventReservation";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return (await conn.QueryAsync<EventReservation>(query)).ToList();
        }

        public async Task <EventReservation> ConsultarReservasIdAsync(long IdReservation)
        {
            var query = "SELECT * FROM EventReservation WHERE IdReservation = @IdReservation";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DynamicParameters parameters = new(new { IdReservation });
            return await conn.QueryFirstOrDefaultAsync<EventReservation>(query, parameters);
        }

        public async Task<List<object>> ConsultarEventosPersonNameTitleAsync(string personName, string title)
        {
            var query = @$"SELECT * FROM EventReservation AS event
                           INNER JOIN CityEvent AS city ON
                           event.PersonName = @personName AND city.Title like ('%' + @title + '%') AND event.IdEvent = city.IdEvent ";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DynamicParameters parameters = new(new { title, personName });
            return (await conn.QueryAsync<object>(query, parameters)).ToList();
        }

        public async Task<bool> CriarReservaAsync(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES (@IdEvent, @PersonName, @Quantity)";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DynamicParameters parameters = new(new { eventReservation.IdEvent, eventReservation.PersonName, eventReservation.Quantity });
            return await conn.ExecuteAsync(query, parameters) == 1;
        }

        public async Task<bool> EditarReservaAsync(long IdReservation, long Quantity)
        {
            var query = @"UPDATE EventReservation SET
                          Quantity = @Quantity
                          WHERE IdReservation = @IdReservation";
            DynamicParameters parameters = new(new { IdReservation, Quantity });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return await conn.ExecuteAsync(query, parameters) == 1;
        }

        public async Task<bool> ExcluirReservaAsync(long IdReservation)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @IdReservation";
            DynamicParameters parameters = new(new { IdReservation });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return await conn.ExecuteAsync(query, parameters) == 1;
        }

        public async Task <List<EventReservation>> RemoveEventAsync(long IdEvent)
        {
            var query = "select * from EventReservation where IdEvent = @IdEvent";
            DynamicParameters parameters = new(new { IdEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return (await conn.QueryAsync<EventReservation>(query, parameters)).ToList();
        }


    }
}

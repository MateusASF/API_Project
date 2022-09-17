using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace APIEvents.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EventReservationRepository> _logger;

        public EventReservationRepository(IConfiguration configuration, ILogger<EventReservationRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<EventReservation>> ConsultarReservasAsync()
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - EventReservationRepository: ConsultarReservasAsync"]);
            var query = "SELECT * FROM EventReservation";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                return (await conn.QueryAsync<EventReservation>(query)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw;
            }
        }

        public async Task <EventReservation> ConsultarReservasIdAsync(long IdReservation)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - EventReservationRepository: ConsultarReservasIdAsync"]);
            var query = "SELECT * FROM EventReservation WHERE IdReservation = @IdReservation";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DynamicParameters parameters = new(new { IdReservation });
            try
            {
                return await conn.QueryFirstOrDefaultAsync<EventReservation>(query, parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw;
            }
        }

        public async Task<List<object>> ConsultarEventosPersonNameTitleAsync(string personName, string title)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - EventReservationRepository: ConsultarEventosPersonNameTitleAsync"]);
            var query = @$"SELECT * FROM EventReservation AS event
                           INNER JOIN CityEvent AS city ON
                           event.PersonName = @personName AND city.Title like ('%' + @title + '%') AND event.IdEvent = city.IdEvent ";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DynamicParameters parameters = new(new { title, personName });
            try
            {
                return (await conn.QueryAsync<object>(query, parameters)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw;
            }
        }

        public async Task<bool> CriarReservaAsync(EventReservation eventReservation)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - EventReservationRepository: CriarReservaAsync"]);
            var query = "INSERT INTO EventReservation VALUES (@IdEvent, @PersonName, @Quantity)";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            DynamicParameters parameters = new(eventReservation);
            try
            {
                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw;
            }
        }

        public async Task<bool> EditarReservaAsync(long IdReservation, long Quantity)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - EventReservationRepository: EditarReservaAsync"]);
            var query = @"UPDATE EventReservation SET
                          Quantity = @Quantity
                          WHERE IdReservation = @IdReservation";
            DynamicParameters parameters = new(new { IdReservation, Quantity });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw;
            }
        }

        public async Task<bool> ExcluirReservaAsync(long IdReservation)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - EventReservationRepository: ExcluirReservaAsync"]);
            var query = "DELETE FROM EventReservation WHERE IdReservation = @IdReservation";
            DynamicParameters parameters = new(new { IdReservation });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw;
            }
        }

        public async Task<List<EventReservation>> RemoveEventAsync(long IdEvent)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - EventReservationRepository: RemoveEventAsync"]);
            var query = "select * from EventReservation where IdEvent = @IdEvent";
            DynamicParameters parameters = new(new { IdEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                return (await conn.QueryAsync<EventReservation>(query, parameters)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw;
            }
        }


    }
}

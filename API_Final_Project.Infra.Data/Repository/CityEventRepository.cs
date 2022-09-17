using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;
using Microsoft.Extensions.Logging;

namespace APIEvents.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CityEventRepository> _logger;

        public CityEventRepository(IConfiguration configuration, ILogger<CityEventRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<CityEvent>> ConsultarEventosAsync()
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - CityEventRepository: ConsultarEventosAsync"]);
            var query = "SELECT * FROM CityEvent WHERE status = 1";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                return (await conn.QueryAsync<CityEvent>(query)).ToList();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw; 
            }
        }

        public async Task<List<CityEvent>> ConsultarEventosNomeAsync(string Title)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - CityEventRepository: ConsultarEventosNomeAsync"]);
            var query = $"SELECT * FROM CityEvent WHERE Title LIKE ('%' +  @Title + '%') ";
            DynamicParameters parameters = new(new { Title });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw;
            }
        }

        public async Task<List<CityEvent>> ConsultarEventosLocalDataAsync(string local, DateTime dateHourEvent)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - CityEventRepository: ConsultarEventosLocalDataAsync"]);
            var query = "SELECT * FROM CityEvent WHERE CAST(dateHourEvent as DATE) = CAST(@dateHourEvent as DATE) and local = @local";
            DynamicParameters parameters = new(new { local, dateHourEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw;
            }
        }

        public async Task<List<CityEvent>> ConsultarEventosPrecoDataAsync(decimal min, decimal max, DateTime dateHourEvent)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - CityEventRepository: ConsultarEventosPrecoDataAsync"]);
            var query = "SELECT * FROM CityEvent WHERE CAST(dateHourEvent as DATE) = CAST(@dateHourEvent as DATE) and price between @min and @max";
            DynamicParameters parameters = new(new { min, max, dateHourEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw;
            }
        }


        public async Task<CityEvent> ConsultarEventosidAsync(long idEvent)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - CityEventRepository: ConsultarEventosidAsync"]);
            var query = "SELECT * FROM CityEvent WHERE idEvent = @idEvent";
            DynamicParameters parameters = new(new { idEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                return await conn.QueryFirstOrDefaultAsync<CityEvent>(query, parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro inesperado na requisão da API no acesso ao Banco de Dados");
                throw;
            }
        }

        public async Task<bool> CriarEventoAsync(CityEvent cityEvent)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - CityEventRepository: CriarEventoAsync"]);
            var query = "INSERT INTO CityEvent VALUES (@Title, @Description, @DateHourEvent, @Local, @Address, @Price, @Status)";
            DynamicParameters parameters = new(cityEvent);
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

        public async Task<bool> EditarEventoAsync(long id, CityEvent cityEvent)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - CityEventRepository: EditarEventoAsync"]);
            var query = @"UPDATE CityEvent SET Title = @Title,
                          Description = @Description,
                          DateHourEvent = @DateHourEvent,
                          Local = @Local,
                          Address = @Address,
                          Price = @Price,
                          Status = @Status  
                          WHERE IdEvent = @id";
            #region Parameters
            DynamicParameters parameters = new();
            parameters.Add("Title", cityEvent.Title);
            parameters.Add("Description", cityEvent.Description);
            parameters.Add("DateHourEvent", cityEvent.DateHourEvent);
            parameters.Add("Local", cityEvent.Local);
            parameters.Add("Address", cityEvent.Address);
            parameters.Add("Price", cityEvent.Price);
            parameters.Add("Status", cityEvent.Status);
            parameters.Add("id", id);
            #endregion
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

        public async Task<bool> ExcluirEventoAsync(long IdEvent)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - CityEventRepository: ExcluirEventoAsync"]);
            var query = "DELETE FROM CityEvent WHERE IdEvent = @IdEvent";
            DynamicParameters parameters = new(new { IdEvent });
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


        public async Task<bool> AlterStatusAsync(long IdEvent)
        {
            _logger.LogInformation("Chamando URL: {0}", _configuration["APIEvents - CityEventRepository: AlterStatusAsync"]);
            var query = @"UPDATE CityEvent SET Status = 0
                          WHERE cityEvent.IdEvent = @IdEvent";
            DynamicParameters parameters = new(new { IdEvent });
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
    }
}

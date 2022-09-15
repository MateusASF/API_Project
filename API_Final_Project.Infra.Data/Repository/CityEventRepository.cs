using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using APIEvents.Core.Interfaces;
using APIEvents.Core.Models;

namespace APIEvents.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CityEvent> ConsultarEventos()
        {
            var query = "SELECT * FROM CityEvent WHERE status = 1";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<CityEvent>(query).ToList();
        }

        public List<CityEvent> ConsultarEventosNome(string Title)
        {
            var query = $"SELECT * FROM CityEvent WHERE Title LIKE ('%' +  @Title + '%') ";
            DynamicParameters parameters = new(new { Title });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> ConsultarEventosLocalData(string local, DateTime dateHourEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE CAST(dateHourEvent as DATE) = CAST(@dateHourEvent as DATE) and local = @local";
            DynamicParameters parameters = new(new { local, dateHourEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var camelo = conn.Query<CityEvent>(query, parameters).ToList();
            return camelo;
        }

        public List<CityEvent> ConsultarEventosPrecoData(decimal min, decimal max, DateTime dateHourEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE CAST(dateHourEvent as DATE) = CAST(@dateHourEvent as DATE) and price between @min and @max";
            DynamicParameters parameters = new(new { min, max, dateHourEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var camelo = conn.Query<CityEvent>(query, parameters).ToList();
            return camelo;
        }


        public CityEvent ConsultarEventosid(long idEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE idEvent = @idEvent";
            DynamicParameters parameters = new(new { idEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
        }

        public bool CriarEvento(CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@Title, @Description, @DateHourEvent, @Local, @Address, @Price, @Status)";
            DynamicParameters parameters = new(new { cityEvent.Title, cityEvent.Description, cityEvent.DateHourEvent, cityEvent.Local, cityEvent.Address, cityEvent.Price, cityEvent.Status });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

        public bool EditarEvento(long Id, CityEvent cityEvent)
        {
            var query = @"UPDATE CityEvent SET Title = @Title,
                          Description = @Description,
                          DateHourEvent = @DateHourEvent,
                          Local = @Local,
                          Address = @Address,
                          Price = @Price
                          Status = @Status
                          WHERE cityEvent.IdEvent = @IdEvent";
            cityEvent.IdEvent = Id;
            DynamicParameters parameters = new(cityEvent);
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

        public bool ExcluirEvento(long IdEvent)
        {
            var query = "DELETE FROM CityEvent WHERE IdEvent = @IdEvent";
            DynamicParameters parameters = new(new { IdEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }


        public bool AlterStatus(long IdEvent)
        {
            var query = @"UPDATE CityEvent SET Status = 0
                          WHERE cityEvent.IdEvent = @IdEvent";
            DynamicParameters parameters = new(new { IdEvent });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }
    }
}

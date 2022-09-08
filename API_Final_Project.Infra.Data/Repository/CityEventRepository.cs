using API_Final_Project.Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using API_Final_Project;


namespace API_Final_Project.Infra.Data.Repository
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
            var query = "SELECT * FROM CityEvent";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Query<CityEvent>(query).ToList();
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
            var query = "INSERT INTO CityEvent VALUES (@Title, @Description, @DateHourEvent, @Local, @Adress, @Price)";
            DynamicParameters parameters = new(new { cityEvent.Title, cityEvent.Description, cityEvent.DateHourEvent, cityEvent.Local, cityEvent.Address, cityEvent.Price });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaulConnection"));
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
                          WHERE cityEvent.IdEvent = @IdEvent";
            cityEvent.IdEvent = Id;
            DynamicParameters parameters = new(cityEvent);
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

        public bool ExcluirEvento(long Id)
        {
            var query = "DELETE FROM EventReservation WHERE IdEvent = @IdEvent";
            DynamicParameters parameters = new(Id);
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn.Execute(query, parameters) == 1;
        }

    }
}

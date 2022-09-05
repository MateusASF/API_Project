using API_Final_Project.Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


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

        public bool CriarEvento (CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@Title, @DescriptionEvet, @DateHourEvent, @LocalEvent, @AdressEvent, @Price)";
            DynamicParameters parameters = new(new { cityEvent.Title, cityEvent.DescriptionEvet, cityEvent.DateHourEvent, cityEvent.LocalEvent, cityEvent.AdressEvent, cityEvent.Price });
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaulConnection"));
            return conn.Execute(query, parameters) == 1;
        }

        public bool EditarEvento(long Id, CityEvent cityEvent)
        {
            var query = @"UPDATE CityEvent SET Title = @Title,
                          DescriptionEvet = @DescriptionEvet,
                          DateHourEvent = @DateHourEvent,
                          LocalEvent = @LocalEvent,
                          AdressEvent = @AdressEvent,
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

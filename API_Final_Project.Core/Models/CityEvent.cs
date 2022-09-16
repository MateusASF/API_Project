using System.Text.Json.Serialization;

namespace APIEvents.Core.Models
{
    public class CityEvent
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public long IdEvent { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateHourEvent { get; set; }
        public string? Local { get; set; }
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public bool Status { get; set; }
    }
}
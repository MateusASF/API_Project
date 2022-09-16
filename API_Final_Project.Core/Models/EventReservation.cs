using System.Text.Json.Serialization;

namespace APIEvents.Core.Models
{
    public class EventReservation
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public long IdReservation { get; private set; }
        public long IdEvent { get; set; }
        public string? PersonName { get; set; }
        public long Quantity { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIEvents.Core.Models
{
    public class EventReservation
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public long IdReservation { get; private set; }


        [Required(ErrorMessage = "O id deve ser obrigatório")]
        public long IdEvent { get; set; }


        [Required(ErrorMessage = "O nome deve ser obrigatório")]
        public string? PersonName { get; set; }


        [Required(ErrorMessage = "A quantidade deve ser obrigatória")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser positivo")]
        public long Quantity { get; set; }
    }
}

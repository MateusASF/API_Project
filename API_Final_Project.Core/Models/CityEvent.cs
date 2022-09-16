using APIEvents.Core.CustomAtributtes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIEvents.Core.Models
{
    public class CityEvent
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public long IdEvent { get;  private set; }


        [Required(ErrorMessage = "O Título deve ser obrigatório")]
        public string? Title { get; set; }


        public string? Description { get; set; }
        

        [Required(ErrorMessage = "A data deve ser obrigatório")]
        [CustomAtributteDate(ErrorMessage = "A data deve ser uma data futura ou atual")]
        public DateTime DateHourEvent { get; set; }


        [Required(ErrorMessage = "O Local deve ser obrigatório")]
        public string? Local { get; set; }


        [Required(ErrorMessage = "O endereço deve ser obrigatório")]
        public string? Address { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "O valor do preço deve ser positivo")]
        public decimal? Price { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Status { get; private set; }
    }
}
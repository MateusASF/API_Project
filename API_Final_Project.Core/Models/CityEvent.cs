namespace API_Final_Project
{
    public class CityEvent
    {
        public long IdEvent { get; set; }
        public string Title { get; set; }
        public string DescriptionEvet { get; set; }
        public DateTime DateHourEvent { get; set; }
        public string LocalEvent { get; set; }
        public string? AdressEvent { get; set; }
        public decimal? Price { get; set; }
    }
}
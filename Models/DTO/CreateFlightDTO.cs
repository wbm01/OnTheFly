namespace OnTheFly.FlightsService.DTOs
{
    public class CreateFlightDTO
    {
        public string IATA { get; set; }
        public string RAB { get; set; }
        public int Sale { get; set; }
        public string Departure { get; set; }
        public bool Status { get; set; }
    }
}

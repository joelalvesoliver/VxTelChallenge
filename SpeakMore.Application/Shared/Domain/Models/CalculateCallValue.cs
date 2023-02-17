namespace SpeakMore.Application.Shared.Domain.Models
{
    public class CalculateCallValue
    {
        public int Origin { get; set; }
        public int Destination { get; set; }
        public int TimeOfCall { get; set; }
        public string PlanName { get; set; }
    }
}

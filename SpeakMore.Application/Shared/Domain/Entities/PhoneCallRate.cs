using System.ComponentModel.DataAnnotations.Schema;

namespace SpeakMore.Application.Shared.Domain.Entities
{
    public class PhoneCallRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Origin { get; set; }
        public int Destination { get; set; }
        public decimal Rate { get; set; }
    }
}

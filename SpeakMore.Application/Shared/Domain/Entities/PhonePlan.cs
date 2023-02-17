using System.ComponentModel.DataAnnotations.Schema;

namespace SpeakMore.Application.Shared.Domain.Entities
{
    public class PhonePlan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Time { get; set; }
        public string Name { get; set; }
    }
}

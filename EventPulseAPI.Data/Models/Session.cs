using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventPulseAPI.Data.Models
{
    public class Session : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public int EventId { get; set; }
        [JsonIgnore]
        public Event Event { get; set; }
        [JsonIgnore]
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}

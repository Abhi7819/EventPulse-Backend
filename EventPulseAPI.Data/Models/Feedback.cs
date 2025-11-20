using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventPulseAPI.Data.Models
{
    public class Feedback : BaseEntity
    {
        public int SessionId { get; set; }
        [JsonIgnore]
        public Session Session { get; set; }
        public int AttendeeId { get; set; }
        [JsonIgnore]
        public User Attendee { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }
    }
}

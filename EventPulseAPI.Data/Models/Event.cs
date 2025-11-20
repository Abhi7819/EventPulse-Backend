using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventPulseAPI.Data.Models
{
    public class Event : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        [JsonIgnore]
        public ICollection<Session> Sessions { get; set; } = new List<Session>();

    }
}

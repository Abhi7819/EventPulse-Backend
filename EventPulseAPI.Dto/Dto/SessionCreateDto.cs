namespace EventPulseAPI.Dto.Dto
{
    public class SessionCreateDto
    {
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EventId { get; set; }
    }
}

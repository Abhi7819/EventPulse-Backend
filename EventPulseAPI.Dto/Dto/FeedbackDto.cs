namespace EventPulseAPI.Dto.Dto
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int AttendeeId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}

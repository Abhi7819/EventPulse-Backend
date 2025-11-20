namespace EventPulseAPI.Dto.Dto
{
    public class FeedbackCreateDto
    {
        public int SessionId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}

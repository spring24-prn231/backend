namespace BusinessObjects.Requests
{
    public class CreateFeedbackRequest
    {

        public Guid OrderId { get; set; }

        public byte RatingStar { get; set; }

        public string? Comment { get; set; }
    }
}

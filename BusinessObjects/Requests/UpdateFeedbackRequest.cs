namespace BusinessObjects.Requests
{
    public class UpdateFeedbackRequest : BaseUpdateRequest
    {

        public byte? RatingStar { get; set; }

        public string? Comment { get; set; }
    }
}

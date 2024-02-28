using BusinessObjects.Requests;
using FluentValidation;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateFeedbackValidator : AbstractValidator<UpdateFeedbackRequest>
    {
        public UpdateFeedbackValidator()
        {
            RuleFor(x => x.RatingStar).LessThanOrEqualTo((byte)5);
        }
    }
}

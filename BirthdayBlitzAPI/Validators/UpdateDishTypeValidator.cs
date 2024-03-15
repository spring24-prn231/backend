using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateDishTypeValidator : AbstractValidator<UpdateDishTypeRequest>
    {
        public UpdateDishTypeValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Loại món ăn không quá 100 kí tự");
        }
    }
}
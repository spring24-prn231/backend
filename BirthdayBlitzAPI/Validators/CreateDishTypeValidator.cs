using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateDishTypeValidator : AbstractValidator<CreateDishTypeRequest>
    {
        public CreateDishTypeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(100).WithMessage("Loại món ăn không quá 100 kí tự");
        }
    }
}
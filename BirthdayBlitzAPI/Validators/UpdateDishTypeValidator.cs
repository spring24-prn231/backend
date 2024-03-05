using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateDishTypeValidator : AbstractValidator<UpdateDishTypeRequest>
    {
        private readonly IDishTypeService _dishTypeService;
        public UpdateDishTypeValidator(IDishTypeService dishTypeService)
        {
            _dishTypeService = dishTypeService;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(100).WithMessage("Loại món ăn không quá 100 kí tự");
        }
    }
}
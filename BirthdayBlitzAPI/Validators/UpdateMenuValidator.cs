using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateMenuValidator : AbstractValidator<UpdateMenuRequest>
    {
        private readonly IMenuService _menuService;
        public UpdateMenuValidator(IMenuService menuService)
        {
            _menuService = menuService;
            RuleFor(x => x.DishId)
                .Must(x => _menuService.GetByIdNoTracking(x) != null)
                .WithMessage("Dish không tồn tại");
            RuleFor(x => x.ServiceId)
                .Must(x => _menuService.GetByIdNoTracking(x) != null)
                .WithMessage("Service không tồn tại");
        }
    }
}
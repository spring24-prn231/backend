using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateMenuValidator : AbstractValidator<CreateMenuRequest>
    {
        private readonly IDishTypeService _dishTypeService;
        private readonly IServiceService _serviceService;
        private readonly IDishService _dishService;
        public CreateMenuValidator(IDishTypeService dishTypeService, IServiceService serviceService, IDishService dishService)
        {
            _dishTypeService = dishTypeService;
            _serviceService = serviceService;
            _dishService = dishService;

            RuleFor(x => x.DishId)
                .Must(x => _dishService.GetByIdNoTracking(x) != null)
                .WithMessage("Món ăn này không có");

            RuleFor(x => x.ServiceId)
                .Must(x => _serviceService.GetByIdNoTracking(x) != null)
                .WithMessage("Dịch vụ này không tồn tại");
        }
    }
}
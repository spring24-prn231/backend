using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateMenuValidator : AbstractValidator<UpdateMenuRequest>
    {
        private readonly IDishTypeService _dishTypeService;
        private readonly IServiceService _serviceService;
        private readonly IDishService _dishService;
        public UpdateMenuValidator(IDishTypeService dishTypeService, IServiceService serviceService, IDishService dishService)
        {
            _dishTypeService = dishTypeService;
            _serviceService = serviceService;
            _dishService = dishService;

            RuleFor(x => x.DishId)
                .Must(x => !x.HasValue || _dishService.GetById(x.Value) != null)
                .WithMessage("Món ăn này không có");

            RuleFor(x => x.ServiceId)
                .Must(x => !x.HasValue || _serviceService.GetById(x.Value) != null)
                .WithMessage("Dịch vụ này không tồn tại");
        }
    }
}
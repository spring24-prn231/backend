using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateDishValidator : AbstractValidator<CreateDishRequest>
    {
        private readonly IDishTypeService _dishTypeService;
        public CreateDishValidator(IDishService dishService, IDishTypeService dishTypeService)
        {
            _dishTypeService = dishTypeService;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên món ăn không được để trống")
                .MaximumLength(100).WithMessage("Tên món không quá 100 kí tự");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Mô tả không được để trống")
                .MaximumLength(255).WithMessage("Mô tả món ăn không quá 255 kí tự");
            RuleFor(x => x.Image)
                .Must(BeAValidUrl).WithMessage("Không đúng định dạng ảnh");
            RuleFor(x => x.DishTypeId)
                .Must(x => _dishTypeService.GetByIdNoTracking(x) != null)
                .WithMessage("Loại món ăn không tồn tại");
        }
        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result)
                   && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
    }
}
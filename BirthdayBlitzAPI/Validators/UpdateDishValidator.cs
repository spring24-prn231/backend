using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateDishValidator : AbstractValidator<UpdateDishRequest>
    {
        public readonly IDishService _dishService;
        public UpdateDishValidator(IDishService dishService)
        {
            _dishService = dishService;
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100).WithMessage("Tên món không quá 100 kí tự");
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(255).WithMessage("Mô tả món ăn không quá 255 kí tự");
            RuleFor(x => x.Image)
                .Must(BeAValidUrl).WithMessage("Không đúng định dạng ảnh");
            RuleFor(x => x.DishTypeId)
                .Must(x => _dishService.GetByIdNoTracking(x) != null)
                .WithMessage("Loại món ăn không tồn tại");
        }
        private bool BeAValidUrl(string url)
        {
            // Kiểm tra xem đường dẫn có đúng định dạng URL không
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result)
                   && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
    }
}
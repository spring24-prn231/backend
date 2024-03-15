using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateDishValidator : AbstractValidator<UpdateDishRequest>
    {
        private readonly IDishTypeService _dishTypeService;
        public UpdateDishValidator(IDishTypeService dishTypeService)
        {
            _dishTypeService = dishTypeService;
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Tên món không quá 100 kí tự");
            RuleFor(x => x.Description)
                .MaximumLength(255).WithMessage("Mô tả món ăn không quá 255 kí tự");
            RuleFor(x => x.Image)
                .Must(BeAValidUrl).WithMessage("Không đúng định dạng ảnh");
            RuleFor(x => x.DishTypeId)
                .MustAsync(async (x, cancellationToken) => await _dishTypeService.GetByIdNoTracking(x) != null)
                .WithMessage("Loại món ăn không tồn tại");
        }
        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result)
                   && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
    }
}
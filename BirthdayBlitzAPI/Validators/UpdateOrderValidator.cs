using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderRequest>
    {
        private readonly IServiceService _serviceService;
        public UpdateOrderValidator(IServiceService serviceService)
        {
            _serviceService = serviceService;
            RuleFor(x => x.ServiceId)
                .MustAsync(async (x, cancellationToken) => !x.HasValue || await _serviceService.GetByIdNoTracking(x.Value) != null)
                .WithMessage("Service không tồn tại");
            RuleFor(x => new { x.EventStart, x.EventEnd }).Must(x => x.EventStart < x.EventEnd)
                .WithMessage("Thời gian kết thúc tiệc phải lớn hơn thời gian bắt đầu tiệc")
                .Must(x => x.EventStart >= DateTime.Now.AddDays(1))
                .WithMessage("Phải đặt tiệc trước 1 ngày");
            RuleFor(x => x.MaxGuest).GreaterThan(0).WithMessage("Số lượng khách tối đa phải lớn hơn 0");
        }
    }
}

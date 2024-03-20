using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {
        private readonly IServiceService _serviceService;
        public CreateOrderValidator(IServiceService serviceService, IRoomTypeService roomTypeService,
            IServiceElementService serviceElementService,
            IDishService dishService)
        {
            _serviceService = serviceService;
            RuleFor(x => new { x.NewService, x.RecommendServiceId })
                .Must(x =>
                    (x.NewService == null && x.RecommendServiceId != null)
                    || (x.NewService != null && x.RecommendServiceId == null)
                ).WithMessage("Order chỉ được chọn 1 trong 2 options: tạo service custom hoặc service recommend");

            RuleFor(x => x.RecommendServiceId)
                .MustAsync(async (x, cancellaionToken) => !x.HasValue || (x.HasValue && await _serviceService.GetByIdNoTracking(x.Value) != null))
                .WithMessage("Service Không tồn tại!");
            RuleFor(x => x.Total).PrecisionScale(20, 1, true).WithMessage("Tổng tiền không hợp lệ!");

            RuleFor(x => new { x.EventStart, x.EventEnd }).Must(x => x.EventStart < x.EventEnd)
                .WithMessage("Thời gian kết thúc tiệc phải lớn hơn thời gian bắt đầu tiệc")
                .Must(x => x.EventStart >= DateTime.Now.AddDays(1))
                .WithMessage("Phải đặt tiệc trước 1 ngày");
            RuleFor(x => x.MaxGuest).GreaterThan(0).WithMessage("Số lượng khách tối đa phải lớn hơn 0");
            RuleFor(x => x.NewService).SetValidator(new ServiceOrderCreateValidator(roomTypeService, serviceElementService, dishService));
        }
    }
    public class ServiceOrderCreateValidator : AbstractValidator<ServiceOrderCreate>
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IServiceElementService _serviceElementService;
        private readonly IDishService _dishService;
        public ServiceOrderCreateValidator(IRoomTypeService roomTypeService,
            IServiceElementService serviceElementService,
            IDishService dishService)
        {
            _roomTypeService = roomTypeService;
            _dishService = dishService;
            _serviceElementService = serviceElementService;
            _roomTypeService = roomTypeService;
            _dishService = dishService;
            _serviceElementService = serviceElementService;
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Tên dịch vụ không được quá 100 ký tự");
            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Mô tả dịch vụ không được quá 255 ký tự");
            RuleFor(x => x.RoomTypeId).MustAsync(async (x, cancellationToken) => await _roomTypeService.GetByIdNoTracking(x) != null)
                .WithMessage("Loại phòng không tồn tại");
            RuleForEach(x => x.ServiceElementIds)
                .ChildRules(child =>
                {
                    child.RuleFor(x => x)
                        .MustAsync(async (y, cancellationToken) => await _serviceElementService.GetByIdNoTracking(y) != null)
                        .WithMessage("ServiceElement không tồn tại");
                });
            RuleForEach(x => x.DishIds)
                .ChildRules(child =>
                {
                    child.RuleFor(x => x)
                        .MustAsync(async (y, cancellationToken) => await _dishService.GetByIdNoTracking(y) != null)
                        .WithMessage("Món ăn không tồn tại trong Menu");
                });
        }
    }
}

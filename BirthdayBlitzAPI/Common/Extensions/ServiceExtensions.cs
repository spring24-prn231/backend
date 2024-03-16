using DataAccessObjects;
using Repositories;
using Services.Implements;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Common.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDI(this IServiceCollection services)
        {
            services.AddSingleton<AzureBlobService>();
            services.AddScoped(typeof(IBaseDAO<>), typeof(BaseDAO<>));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDepositService, DepositService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IDishTypeService, DishTypeService>();
            services.AddScoped<IElementTypeService, ElementTypeService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IPartyPlanService, PartyPlanService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IServiceElementService, ServiceElementService>();
            services.AddScoped<IServiceElementDetailService, ServiceElementDetailService>();
            services.AddScoped<IVoucherService, VoucherService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<INotificationService, NotificationService>();
        }
    }
}

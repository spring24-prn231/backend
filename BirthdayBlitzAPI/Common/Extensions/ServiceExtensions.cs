using DataAccessObjects;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services.Implements;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayBlitzAPI.Common.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDI(this IServiceCollection services)
        {
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
        }
    }
}

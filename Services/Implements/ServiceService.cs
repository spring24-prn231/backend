using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class ServiceService : BaseService<Service>, IServiceService
    {
        private readonly BirthdayBlitzContext _context;
        private readonly IMapper _mapper;
        public ServiceService(IBaseRepository<Service> repository, IMapper mapper, BirthdayBlitzContext context) : base(repository, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Update(UpdateServiceRequest request)
        {
            var service = _context.Services.Find(request.Id);
            _mapper.Map(request, service);

            foreach (var elementId in request.ServiceElementIds)
            {
                var serviceElementDetail = new ServiceElementDetail
                {
                    ServiceId = service.Id,
                    ServiceElementId = elementId
                };
                _context.ServiceElementDetails.Add(serviceElementDetail);
            }

            foreach (var dishId in request.DishIds)
            {
                var menu = new Menu
                {
                    ServiceId = service.Id,
                    DishId = dishId
                };
                _context.Menus.Add(menu);
            }

            _context.SaveChanges();
        }
    }
}

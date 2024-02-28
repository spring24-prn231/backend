using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class VoucherService : BaseService<Voucher>, IVoucherService
    {
        public VoucherService(IBaseRepository<Voucher> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}

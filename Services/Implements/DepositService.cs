using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class DepositService : BaseService<Deposit>, IDepositService
    {
        public DepositService(IBaseRepository<Deposit> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}

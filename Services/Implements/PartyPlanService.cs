using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class PartyPlanService : BaseService<PartyPlan>, IPartyPlanService
    {
        public PartyPlanService(IBaseRepository<PartyPlan> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}

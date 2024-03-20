using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class PartyPlanService : BaseService<PartyPlan>, IPartyPlanService
    {
        public PartyPlanService(IBaseRepository<PartyPlan> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task UpdateList(List<UpdatePartyPlanRequestList> request)
        {
            foreach (var plan in request)
            {
                if (plan.Id.HasValue)
                {
                    var updateReq = _mapper.Map<UpdatePartyPlanRequest>(plan);
                    await Update(updateReq);
                }
                else
                {
                    var insertReq = _mapper.Map<CreatePartyPlanRequest>(plan);
                    await Create(insertReq);
                }
            }
        }
    }
}

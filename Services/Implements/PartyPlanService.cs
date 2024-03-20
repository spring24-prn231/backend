using AutoMapper;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using Repositories;
using Services.Interfaces;
using FluentValidation.Results;

namespace Services.Implements
{
    public class PartyPlanService : BaseService<PartyPlan>, IPartyPlanService
    {
        public PartyPlanService(IBaseRepository<PartyPlan> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task UpdateList(UpdatePartyPlanRequestList request)
        {
            var partyPlansForOrder = _repo.GetAll().GetQueryStatusTrue().Where(x => x.OrderId == request.OrderId).ToList();
            foreach (var plan in partyPlansForOrder)
            {
                await _repo.HardDelete(plan);
            }
            foreach (var plan in request.PartyPlans)
            {
                var insertReq = _mapper.Map<CreatePartyPlanRequest>(plan);
                insertReq.OrderId = request.OrderId;
                await Create(insertReq);
            }
        }
    }
}

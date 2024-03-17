using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class PartyPlanAssignmentService : BaseService<PartyPlanAssignment>, IPartyPlanAssignmentService
    {
        public PartyPlanAssignmentService(IMapper mapper, IBaseRepository<PartyPlanAssignment> repository) : base(repository, mapper)
        {

        }

        public override async Task Create<TReq>(TReq entity)
        {
            if (entity is AssignStaffPlanRequest entityList)
            {
                foreach (var staffId in entityList.StaffIds)
                {
                    await _repo.Create(new PartyPlanAssignment
                    {
                        StaffId = staffId,
                        PartyPlanId = entityList.PlanId,
                    });
                }
            }
        }
    }
}

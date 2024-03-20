using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace Services.Interfaces
{
    public interface IPartyPlanService : IBaseService<PartyPlan>
    {
        Task UpdateList(UpdatePartyPlanRequestList request);
    }
}

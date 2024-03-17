using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace Services.Interfaces
{
    public interface IServiceService : IBaseService<Service>
    {
        Task Create<Treq>(Treq entityRequest) where Treq : class;
    }
}

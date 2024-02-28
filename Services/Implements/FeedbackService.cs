using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class FeedbackService : BaseService<Feedback>, IFeedbackService
    {
        public FeedbackService(IBaseRepository<Feedback> repository, IMapper mapper) : base(repository, mapper) { }
    }
}

using AutoFilterer.Abstractions;
using AutoMapper;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implements
{
    public class FeedbackService : BaseService<Feedback>, IFeedbackService
    {
        public FeedbackService(IBaseRepository<Feedback> repository, IMapper mapper) : base(repository, mapper) { }
    }
}

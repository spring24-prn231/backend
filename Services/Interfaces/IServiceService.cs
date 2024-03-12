﻿using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace Services.Interfaces
{
    public interface IServiceService : IBaseService<Service>
    {
        public void Update(UpdateServiceRequest request);
    }
}

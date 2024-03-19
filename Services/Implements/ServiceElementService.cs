using AutoMapper;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class ServiceElementService : BaseService<ServiceElement>, IServiceElementService
    {
        private readonly AzureBlobService _azureBlobService;
        public ServiceElementService(IBaseRepository<ServiceElement> repository, IMapper mapper, AzureBlobService azureBlobService) : base(repository, mapper)
        {
            _azureBlobService = azureBlobService;
        }
        public override async Task Create<TReq>(TReq entity)
        {
            if(entity is CreateServiceElementRequest newEntityReq)
            {
                var newEntity = _mapper.Map<ServiceElement>(newEntityReq);
                var imageName = $"ServiceElement_{newEntity.Id}.{Path.GetExtension(newEntityReq.ImageFile.FileName)}";
                var response = await _azureBlobService.UploadFiles(new List<IFormFile> { newEntityReq.ImageFile }, imageName);
                newEntity.Image = response.FirstOrDefault();
                await _repo.Create(newEntity);

            }
            else
            {
                throw new ValidationException();
            }
        }

        public override async Task Update<TReq>(TReq entityRequest)
        {
            if (entityRequest is UpdateServiceElementRequest serviceElementUpdate)
            {
                var entity = await _repo.GetById(entityRequest.Id);
                if (entity != null)
                {
                    if (serviceElementUpdate.ImageFile != null)
                    {
                        var fileName = $"ServiceElement_{serviceElementUpdate.Id}.{Path.GetExtension(serviceElementUpdate.ImageFile.FileName)}";
                        var contractLinks = await _azureBlobService.UploadFiles(new List<IFormFile> { serviceElementUpdate.ImageFile }, fileName);
                        entity.Image = contractLinks.FirstOrDefault();
                    }
                    _mapper.Map(entityRequest, entity);
                    await _repo.Update(entity);
                }
                else
                {
                    throw new NotFoundException();
                }
            }
            else
            {
                throw new ValidationException();
            }
        }
    }
}

using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using Repositories;
using Services.Interfaces;
using Microsoft.AspNetCore.Http;
using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Exceptions;

namespace Services.Implements
{
    public class DishService : BaseService<Dish>, IDishService
    {
        private readonly AzureBlobService _azureBlobService;
        public DishService(IBaseRepository<Dish> repository, IMapper mapper, AzureBlobService azureBlobService) : base(repository, mapper)
        {
            _azureBlobService = azureBlobService;
        }

        public override async Task<Dish> Create<TReq>(TReq entity)
        {
            if (entity is CreateDishRequest newEntityReq)
            {
                var newEntity = _mapper.Map<Dish>(newEntityReq);
                var imageName = $"Dish_{newEntity.Id}.{Path.GetExtension(newEntityReq.ImageFile.FileName)}";
                var response = await _azureBlobService.UploadFiles(new List<IFormFile> { newEntityReq.ImageFile }, imageName);
                newEntity.Image = response.FirstOrDefault();
                return await _repo.Create(newEntity);
            }
            else
            {
                throw new ValidationException();
            }
        }

        public override async Task<Dish> Update<TReq>(TReq entityRequest)
        {
            if (entityRequest is UpdateDishRequest dishUpdate)
            {
                var entity = await _repo.GetById(entityRequest.Id);
                if (entity != null)
                {
                    if(dishUpdate.ImageFile != null)
                    {
                        var fileName = $"Dish_{dishUpdate.Id}.{Path.GetExtension(dishUpdate?.ImageFile.FileName)}";
                        var contractLinks = await _azureBlobService.UploadFiles(new List<IFormFile> { dishUpdate.ImageFile }, fileName);
                        entity.Image = contractLinks.FirstOrDefault();
                    }
                    _mapper.Map(entityRequest, entity);
                    return await _repo.Update(entity);
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

using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using Repositories;
using Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Services.Implements
{
    public class DishService : BaseService<Dish>, IDishService
    {
        private readonly AzureBlobService _azureBlobService;
        public DishService(IBaseRepository<Dish> repository, IMapper mapper, AzureBlobService azureBlobService) : base(repository, mapper)
        {
            _azureBlobService = azureBlobService;
        }

        public override async Task Create<TReq>(TReq entity)
        {
            var newEntityReq = entity as CreateDishRequest;
            var newEntity = _mapper.Map<Dish>(newEntityReq);
            var imageName = $"Dish_{newEntity.Id}.{Path.GetExtension(newEntityReq.ImageFile.FileName)}";
            var reponse = await _azureBlobService.UploadFiles(new List<IFormFile> { newEntityReq.ImageFile }, imageName);
            newEntity.Image = $"https://birthdayblitzfilestorage.blob.core.windows.net/images/{imageName}";
            await _repo.Create(newEntity);
        }
    }
}

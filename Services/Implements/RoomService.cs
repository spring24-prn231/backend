using AutoMapper;
using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class RoomService : BaseService<Room>, IRoomService
    {
        private readonly AzureBlobService _azureBlobService;
        public RoomService(IBaseRepository<Room> repository, IMapper mapper, AzureBlobService azureBlobService) : base(repository, mapper)
        {
            _azureBlobService = azureBlobService;
        }
        public override async Task<Room> Create<TReq>(TReq entity)
        {
            if(entity is CreateRoomRequest newEntityReq)
            {
                var newEntity = _mapper.Map<Room>(newEntityReq);
                var imageName = $"Room_{newEntity.Id}.{Path.GetExtension(newEntityReq.ImageFile.FileName)}";
                var response = await _azureBlobService.UploadFiles(new List<IFormFile> { newEntityReq.ImageFile }, imageName);
                newEntity.Image = response.FirstOrDefault();
                return await _repo.Create(newEntity);
            }
            else
            {
                throw new ValidationException();
            }
        }

        public override async Task<Room> Update<TReq>(TReq entityRequest)
        {
            if (entityRequest is UpdateRoomRequest roomUpdate)
            {
                var entity = await _repo.GetById(entityRequest.Id);
                if (entity != null)
                {
                    if (roomUpdate.ImageFile != null)
                    {
                        var fileName = $"Room_{roomUpdate.Id}.{Path.GetExtension(roomUpdate?.ImageFile.FileName)}";
                        var contractLinks = await _azureBlobService.UploadFiles(new List<IFormFile> { roomUpdate.ImageFile }, fileName);
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

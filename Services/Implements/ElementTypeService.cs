using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class ElementTypeService : BaseService<ElementType>, IElementTypeService
    {
        public ElementTypeService(IBaseRepository<ElementType> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}

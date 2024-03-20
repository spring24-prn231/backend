using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class UpdateDishRequest : BaseUpdateRequest  
    {
        public Guid DishTypeId { get; set; }
        public string? Name { get; set; }
        public IFormFile? ImageFile { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
    }
}
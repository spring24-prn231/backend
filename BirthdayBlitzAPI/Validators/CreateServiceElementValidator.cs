using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateServiceElementValidator : AbstractValidator<CreateServiceElementRequest>
    {
        private bool isImageUrlValid(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
        private readonly IServiceElementDetailService _serviceElementDetailService;
        public CreateServiceElementValidator(IServiceElementDetailService serviceElementDetailService)
        {
            _serviceElementDetailService = serviceElementDetailService;
            RuleFor(x => x.Image).Must(isImageUrlValid)
                .WithMessage("Ảnh không hợp lệ");
            RuleFor(x => x.ElementTypeId).Must(x => _serviceElementDetailService.GetByIdNoTracking(x) != null)
                .WithMessage("ElementType không tồn tại");
        }
    }
}
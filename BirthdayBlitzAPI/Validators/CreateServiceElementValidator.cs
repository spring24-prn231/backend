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
        private readonly IElementTypeService _serviceElementTypeService;
        public CreateServiceElementValidator(IElementTypeService serviceElementTypeService)
        {
            _serviceElementTypeService = serviceElementTypeService;

            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Mô tả không được quá 500 ký tự.");

            RuleFor(x => x.ElementTypeId).MustAsync(async (x, cancellationToken) => await _serviceElementTypeService.GetByIdNoTracking(x) != null)
                .WithMessage("ElementType không tồn tại");
        }
    }
}
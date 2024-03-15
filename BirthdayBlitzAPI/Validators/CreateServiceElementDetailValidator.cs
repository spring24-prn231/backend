using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateServiceElementDetailValidator : AbstractValidator<CreateServiceElementDetailRequest>
    {
        private readonly IServiceService _serviceService;
        private readonly IServiceElementService _serviceElementService;
        public CreateServiceElementDetailValidator(IServiceService serviceService, IServiceElementService serviceElementService)
        {
            _serviceService = serviceService;
            _serviceElementService = serviceElementService;
            RuleFor(x => x.ServiceId).MustAsync(async (x, cancellationToken) => await _serviceService.GetByIdNoTracking(x) != null)
                .WithMessage("Service không tồn tại");
            RuleFor(x => x.ServiceElementId).MustAsync(async (x, cancellationToken) => await _serviceElementService.GetByIdNoTracking(x) != null)
                .WithMessage("ServiceElement không tồn tại");
        }
    }
}
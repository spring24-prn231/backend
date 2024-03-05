using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateServiceValidator : AbstractValidator<UpdateServiceRequest>
    {
        public UpdateServiceValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Tên dịch vụ không được quá 100 ký tự");
            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Mô tả dịch vụ không được quá 255 kýtự");
        }
    }
}
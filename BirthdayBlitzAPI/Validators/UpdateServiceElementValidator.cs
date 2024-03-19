using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateServiceElementValidator : AbstractValidator<UpdateServiceElementRequest>
    {
        public UpdateServiceElementValidator()
        {
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Mô tả không được quá 500 ký tự.");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateElementTypeValidator : AbstractValidator<UpdateElementTypeRequest>
    {
        public UpdateElementTypeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống!")
                .MaximumLength(100)
                .WithMessage("Tên không được quá 100 ký tự!");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateElementTypeValidator : AbstractValidator<CreateElementTypeRequest>
    {
        public CreateElementTypeValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Tên không được quá 100 ký tự!");
        }
    }
}
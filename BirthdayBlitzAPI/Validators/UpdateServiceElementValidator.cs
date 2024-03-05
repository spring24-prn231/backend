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
        private bool isImageUrlValid(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
        public UpdateServiceElementValidator()
        {
            RuleFor(x => x.Image).Must(isImageUrlValid)
                .WithMessage("Ảnh không hợp lệ");
        }
    }
}
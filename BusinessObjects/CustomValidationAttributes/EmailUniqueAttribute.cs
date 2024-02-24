using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.CustomValidationAttributes
{
    public class EmailUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = new BirthdayBlitzContext();
            if (!context.Users.Any(a => a.Email == value.ToString()))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Email exists");
        }

    }
}

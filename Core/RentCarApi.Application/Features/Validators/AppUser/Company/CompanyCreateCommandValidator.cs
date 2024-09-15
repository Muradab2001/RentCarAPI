using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.SignUp;
using RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.Update;
using RentCarApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Validators.AppUser.Company
{
    public class CompanyCreateCommandValidator :AbstractValidator<AppUserCompanySignUpCommandRequest>
    {
        public CompanyCreateCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.RepeatPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.CompanyData)
                .NotNull().WithMessage("Company data is required.");

            RuleFor(x => x.CompanyData.Name)
                .NotEmpty().WithMessage("Company name is required.");

            RuleFor(x => x.CompanyData.Image)
                .NotNull().WithMessage("Company image is required.")
                .Must(BeAValidImage).WithMessage("Invalid image file.");

            RuleFor(x => x.LocationId)
                .GreaterThan(0).WithMessage("Location ID must be a positive number.");
        }

        private bool BeAValidImage(IFormFile file)
        {
            if (file == null) return false;

            string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();

            return !string.IsNullOrEmpty(extension) && permittedExtensions.Contains(extension);
        }
    }
}

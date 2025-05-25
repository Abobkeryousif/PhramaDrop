using FluentValidation;
using PharmaDrop.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Aplication.Validtor
{
    public class UserDtoValidtor : AbstractValidator<UserDto>
    {
        public UserDtoValidtor()
        {
            RuleFor(f => f.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(l => l.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Invalid phone number format.");

            RuleFor(b => b.BirthDay)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(cp => cp.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match.");
     
        }
    }
    }


using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(200).WithMessage("Full name cannot exceed 200 characters.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(100).WithMessage("Username cannot exceed 100 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.RoleId)
                .Must(id => id == 1 || id == 2)
                .WithMessage("RoleId must be either 1 (Admin) or 2 (Worker).");

            When(x => x.RoleId == 2, () =>
            {
                RuleFor(x => x.JobId)
                    .NotNull().WithMessage("Job is required for workers");
            });

            When(x => x.RoleId == 1, () =>
            {
                RuleFor(x => x.JobId)
                    .Must(x => x == null)
                    .WithMessage("Admin cannot have a job assigned");
            });
        }
    }
}

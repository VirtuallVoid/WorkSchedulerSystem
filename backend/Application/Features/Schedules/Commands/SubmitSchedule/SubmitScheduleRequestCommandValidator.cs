using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedules.Commands.SubmitSchedule
{
    public class SubmitScheduleRequestCommandValidator : AbstractValidator<SubmitScheduleRequestCommand>
    {
        public SubmitScheduleRequestCommandValidator()
        {
            RuleFor(x => x.JobId)
                .GreaterThan(0).WithMessage("JobId is required.");

            RuleFor(x => x.ShiftTypeId)
                .GreaterThan(0).WithMessage("ShiftTypeId is required.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("StartDate must be earlier than EndDate.");
        }
    }

}

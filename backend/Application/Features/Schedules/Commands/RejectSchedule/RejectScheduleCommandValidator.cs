using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedules.Commands.RejectSchedule
{
    using FluentValidation;

    public class RejectScheduleCommandValidator : AbstractValidator<RejectScheduleCommand>
    {
        public RejectScheduleCommandValidator()
        {
            RuleFor(x => x.ScheduleId)
                .GreaterThan(0).WithMessage("ScheduleId is required.");
        }
    }

}

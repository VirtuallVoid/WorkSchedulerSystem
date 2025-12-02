using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedules.Commands.ApproveSchedule
{
    using FluentValidation;

    public class ApproveScheduleCommandValidator : AbstractValidator<ApproveScheduleCommand>
    {
        public ApproveScheduleCommandValidator()
        {
            RuleFor(x => x.ScheduleId)
                .GreaterThan(0).WithMessage("ScheduleId is required.");
        }
    }

}

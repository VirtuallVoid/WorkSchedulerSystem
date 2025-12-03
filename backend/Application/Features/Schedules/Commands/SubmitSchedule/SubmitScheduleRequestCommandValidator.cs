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
            RuleFor(x => x.ShiftTypeId)
                .GreaterThan(0).WithMessage("ShiftTypeId is required.");
        }
    }

}

using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedules.Commands.SubmitSchedule
{
    public class SubmitScheduleRequestCommand : IRequest<Response<string>>
    {
        public int ShiftTypeId { get; set; }
        public DateTime ShiftDate { get; set; }
    }

}

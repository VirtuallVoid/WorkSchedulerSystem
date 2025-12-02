using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedules.Commands.ApproveSchedule
{
    public class ApproveScheduleCommand : IRequest<Response<string>>
    {
        public int ScheduleId { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Schedules.Requests
{
    public class ScheduleRequestDto
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public int ShiftTypeId { get; set; }
        public DateTime ShiftDate { get; set; }
    }

}

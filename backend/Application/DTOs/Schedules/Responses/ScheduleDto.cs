using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Schedules.Responses
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public int ShiftTypeId { get; set; }
        public string ShiftTypeName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string? ApprovedByFullName { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}

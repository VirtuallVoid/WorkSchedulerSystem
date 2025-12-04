using Application.DTOs.Schedules.Requests;
using Application.DTOs.Schedules.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISchedulesRepository
    {
        Task<IEnumerable<JobTypesDto>> GetJobTypesAsync();
        Task<IEnumerable<ShiftTypesDto>> GetShiftTypesAsync();
        Task<int> SubmitScheduleRequestAsync(ScheduleRequestDto obj);
        Task<IEnumerable<ScheduleDto>> GetUserSchedulesAsync(int userId);
        Task<IEnumerable<ScheduleDto>> GetAllSchedulesAsync();
        Task<bool> ApproveScheduleAsync(int scheduleId, int adminId);
        Task<bool> RejectScheduleAsync(int scheduleId, int adminId);
    }
}

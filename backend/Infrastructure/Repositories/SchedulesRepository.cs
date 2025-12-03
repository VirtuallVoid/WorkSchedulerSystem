using Application.DTOs.Schedules;
using Application.DTOs.Schedules.Responses;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Dapper;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SchedulesRepository(IDatabaseConfig dbConfig) : ISchedulesRepository
    {
        public async Task<IEnumerable<JobTypesDto>> GetJobTypesAsync()
        {
            const string sql = "EXEC [dbo].[spGetAllJobs]";
            await using var conn = new SqlConnection(dbConfig.ConnectionString);
            return await conn.QueryAsync<JobTypesDto>(sql);
        }
        public async Task<IEnumerable<ShiftTypesDto>> GetShiftTypesAsync()
        {
            const string sql = "EXEC [dbo].[spGetAllShifts]";
            await using var conn = new SqlConnection(dbConfig.ConnectionString);
            return await conn.QueryAsync<ShiftTypesDto>(sql);
        }

        public async Task<int> SubmitScheduleRequestAsync(ScheduleRequestDto obj)
        {
            const string sql = "EXEC [dbo].[spSubmitScheduleRequest] @UserId, @JobId, @ShiftTypeId, @ShiftDate";
            await using var conn = new SqlConnection(dbConfig.ConnectionString);
            return await conn.ExecuteScalarAsync<int>(sql, obj);
        }

        public async Task<IEnumerable<ScheduleDto>> GetUserSchedulesAsync(int userId) // this is worker view
        {
            const string sql = "EXEC [dbo].[spGetUserSchedules] @UserId";
            await using var conn = new SqlConnection(dbConfig.ConnectionString);
            return await conn.QueryAsync<ScheduleDto>(sql, new { UserId = userId });
        }

        public async Task<IEnumerable<ScheduleDto>> GetAllSchedulesAsync() // this is admin view
        {
            const string sql = "EXEC [dbo].[spGetAllSchedules]";
            await using var conn = new SqlConnection(dbConfig.ConnectionString);
            return await conn.QueryAsync<ScheduleDto>(sql);
        }

        public async Task<bool> ApproveScheduleAsync(int scheduleId, int adminId)
        {
            const string sql = "EXEC [dbo].[spApproveSchedule] @ScheduleId, @AdminId";
            await using var conn = new SqlConnection(dbConfig.ConnectionString);
            await conn.ExecuteAsync(sql, new { ScheduleId = scheduleId, AdminId = adminId });
            return true;
        }

        public async Task<bool> RejectScheduleAsync(int scheduleId, int adminId)
        {
            const string sql = "EXEC [dbo].[spRejectSchedule] @ScheduleId, @AdminId";
            await using var conn = new SqlConnection(dbConfig.ConnectionString);
            await conn.ExecuteAsync(sql, new { ScheduleId = scheduleId, AdminId = adminId });
            return true;
        }
    }
}

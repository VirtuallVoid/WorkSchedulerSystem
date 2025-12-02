using Application.DTOs.Logs;
using Application.DTOs.Schedules.Responses;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedules.Queries.GetAllSchedules
{
    public class GetAllSchedulesQueryHandler : IRequestHandler<GetAllSchedulesQuery, Response<List<ScheduleDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public GetAllSchedulesQueryHandler(IUnitOfWork uow, IUserContext userContext)
        {
            _uow = uow;
            _userContext = userContext;
        }

        public async Task<Response<List<ScheduleDto>>> Handle(GetAllSchedulesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var schedules = await _uow.SchedulesRepository.GetAllSchedulesAsync();

                if (schedules == null || !schedules.Any())
                    return Response<List<ScheduleDto>>.Fail(404, "No schedules found");

                return Response<List<ScheduleDto>>.Success(schedules.ToList(), 200, "All schedules retrieved successfully");
            }
            catch (AppException) { throw;}
            catch (Exception ex)
            {
                await ExceptionHelper.HandleExceptionAsync(
                    new LogRequestDto("GetAllSchedules", "Failed to retrieve schedules"),
                    ex,
                    _uow.LoggingRepository
                );
                throw;
            }
        }
    }
}

using Application.DTOs.Logging.Requests;
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

namespace Application.Features.Schedules.Queries.GetUserSchedule
{
    public class GetUserSchedulesQueryHandler : IRequestHandler<GetUserSchedulesQuery, Response<List<ScheduleDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public GetUserSchedulesQueryHandler(IUnitOfWork uow, IUserContext userContext)
        {
            _uow = uow;
            _userContext = userContext;
        }

        public async Task<Response<List<ScheduleDto>>> Handle(GetUserSchedulesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _userContext.UserId != 0 ? _userContext.UserId : throw new AuthException("User not authenticated", "UNAUTHORIZED");
                var schedules = await _uow.SchedulesRepository.GetUserSchedulesAsync(userId);

                if (schedules == null || !schedules.Any())
                    return Response<List<ScheduleDto>>.Fail(404, "No schedules found");

                return Response<List<ScheduleDto>>.Success(schedules.ToList(), 200, "Schedules retrieved successfully");
            }
            catch (AppException) { throw; }
            catch (Exception ex)
            {
                await ExceptionHelper.HandleExceptionAsync(
                    new LogRequestDto("GetUserSchedules", "Failed to retrieve schedules"),
                    ex,
                    _uow.LoggingRepository
                );
                throw;
            }
        }
    }
}

using Application.DTOs.Logs;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedules.Queries.SubmitSchedule
{
    public class SubmitScheduleRequestCommandHandler : IRequestHandler<SubmitScheduleRequestCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public SubmitScheduleRequestCommandHandler(IUnitOfWork uow, IUserContext userContext)
        {
            _uow = uow;
            _userContext = userContext;
        }

        public async Task<Response<string>> Handle(SubmitScheduleRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _userContext.UserId != 0 ? _userContext.UserId :  throw new AuthException("User not authenticated", "UNAUTHORIZED");

                var schedule = new Schedule
                {
                    UserId = userId,
                    JobId = request.JobId,
                    ShiftTypeId = request.ShiftTypeId,
                    StatusId = 1, // Pending
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    RequestDate = DateTime.Now
                };

                var result = await _uow.SchedulesRepository.SubmitScheduleRequestAsync(schedule);

                if (result == 0)
                    return Response<string>.Fail(500, "Failed to submit schedule request");

                return Response<string>.Success("Request submitted", 200, "Schedule request created successfully");
            }
            catch (AppException) { throw; }
            catch (Exception ex)
            {
                await ExceptionHelper.HandleExceptionAsync(
                    new LogRequestDto("SubmitScheduleRequest", "Failed to submit schedule request")
                    {
                        Details = $"JobId: {request.JobId}, Shift: {request.ShiftTypeId}"
                    },
                    ex,
                    _uow.LoggingRepository
                );

                throw;
            }
        }
    }
}

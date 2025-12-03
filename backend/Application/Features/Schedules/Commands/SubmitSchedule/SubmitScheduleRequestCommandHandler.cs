using Application.DTOs.Logs;
using Application.DTOs.Schedules;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedules.Commands.SubmitSchedule
{
    public class SubmitScheduleRequestCommandHandler : IRequestHandler<SubmitScheduleRequestCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public SubmitScheduleRequestCommandHandler(IUnitOfWork uow, IUserContext userContext, IMapper mapper)
        {
            _uow = uow;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(SubmitScheduleRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _userContext.UserId != 0 ? _userContext.UserId :  throw new AuthException("User not authenticated", "UNAUTHORIZED");
                var userInfo = await _uow.AuthRepository.GetUserInfoByUserId(userId);
                var schedule = new ScheduleRequestDto
                {
                    UserId = userId,
                    JobId = userInfo.JobId,
                    ShiftTypeId = request.ShiftTypeId,
                    ShiftDate = request.ShiftDate
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
                        Details = $"UserId: {_userContext.UserId}, Shift: {request.ShiftTypeId}"
                    },
                    ex,
                    _uow.LoggingRepository
                );

                throw;
            }
        }
    }
}

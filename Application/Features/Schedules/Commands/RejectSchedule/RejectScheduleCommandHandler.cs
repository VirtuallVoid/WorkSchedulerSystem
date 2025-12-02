using Application.DTOs.Logs;
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

namespace Application.Features.Schedules.Commands.RejectSchedule
{
    public class RejectScheduleCommandHandler : IRequestHandler<RejectScheduleCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _user;

        public RejectScheduleCommandHandler(IUnitOfWork uow, IUserContext user)
        {
            _uow = uow;
            _user = user;
        }

        public async Task<Response<string>> Handle(RejectScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var adminId = _user.UserId;
                var success = await _uow.SchedulesRepository.RejectScheduleAsync(request.ScheduleId, adminId);

                if (!success)
                    return Response<string>.Fail(400, "Failed to reject schedule");

                return Response<string>.Success("Schedule rejected successfully", 200, "success");
            }
            catch (AppException) { throw; }
            catch (Exception ex)
            {
                await ExceptionHelper.HandleExceptionAsync(
                    new LogRequestDto("RejectSchedule", "Failed to reject schedule")
                    {
                        Details = $"ScheduleId: {request.ScheduleId}"
                    },
                    ex,
                    _uow.LoggingRepository
                );

                throw;
            }
        }
    }
}

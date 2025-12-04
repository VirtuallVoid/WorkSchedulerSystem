using Application.DTOs.Logging.Requests;
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

namespace Application.Features.Schedules.Commands.ApproveSchedule
{
    public class ApproveScheduleCommandHandler : IRequestHandler<ApproveScheduleCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _user;

        public ApproveScheduleCommandHandler(IUnitOfWork uow, IUserContext user)
        {
            _uow = uow;
            _user = user;
        }

        public async Task<Response<string>> Handle(ApproveScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var adminId = _user.UserId;
                var success = await _uow.SchedulesRepository.ApproveScheduleAsync(request.ScheduleId, adminId);

                if (!success)
                    return Response<string>.Fail(400, "Failed to approve schedule");

                return Response<string>.Success("Schedule approved successfully", 200, "success");
            }
            catch (AppException) { throw; }
            catch (Exception ex)
            {
                await ExceptionHelper.HandleExceptionAsync(
                    new LogRequestDto("ApproveSchedule", "Failed to approve schedule")
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

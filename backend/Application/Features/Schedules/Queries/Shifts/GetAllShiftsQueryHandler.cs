using Application.DTOs.Logs;
using Application.DTOs.Schedules.Responses;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedules.Queries.Shifts
{
    public class GetAllShiftsQueryHandler : IRequestHandler<GetAllShiftsQuery, Response<List<ShiftTypesDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllShiftsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<ShiftTypesDto>>> Handle(GetAllShiftsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var shifts = await _uow.SchedulesRepository.GetShiftTypesAsync();

                if (shifts == null || !shifts.Any())
                    return Response<List<ShiftTypesDto>>.Fail(404, "No Shifts Found");

                return Response<List<ShiftTypesDto>>.Success(shifts.ToList(), 200, "Shifts retrieved successfully");
            }
            catch (AppException) { throw; }
            catch (Exception ex)
            {
                await ExceptionHelper.HandleExceptionAsync(
                    new LogRequestDto("GetAllShifts", "Failed to retrieve shift catalog")
                    {
                        Details = ""
                    },
                    ex,
                    _uow.LoggingRepository);

                throw;
            }
        }
    }
}

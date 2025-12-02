using Application.DTOs.Schedules.Responses;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedules.Queries.Shifts
{
    public record GetAllShiftsQuery : IRequest<Response<List<ShiftTypesDto>>>;

}

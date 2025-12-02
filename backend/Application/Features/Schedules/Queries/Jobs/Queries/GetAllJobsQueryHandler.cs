using Application.DTOs.Logs;
using Application.DTOs.Schedules.Responses;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedules.Queries.Jobs.Queries
{
    public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, Response<List<JobTypesDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllJobsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<JobTypesDto>>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var jobs = await _uow.SchedulesRepository.GetJobTypesAsync();

                if (jobs == null || !jobs.Any())
                    return Response<List<JobTypesDto>>.Fail( 404, "No Jobs Found" );

                return Response<List<JobTypesDto>>.Success(jobs.ToList(), 200, "Jobs retrieved successfully");
            }
            catch (AppException) { throw; }
            catch (Exception ex)
            {
                await ExceptionHelper.HandleExceptionAsync(
                    new LogRequestDto("GetAllJobs", "Failed to retrieve job catalog")
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

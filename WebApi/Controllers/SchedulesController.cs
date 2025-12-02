using Application.DTOs.Schedules.Responses;
using Application.Features.Schedules.Queries.Jobs;
using Application.Features.Schedules.Queries.Shifts;
using Application.Features.Schedules.Queries.SubmitSchedule;
using Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers
{
    public class SchedulesController : BaseApiController
    {
        [Authorize]
        [HttpGet("jobs")]
        [SwaggerResponse(200, "Success", typeof(Response<List<JobTypesDto>>))]
        [SwaggerOperation(Summary = "Get Jobs Catalog", Description = "Returns a list of all available jobs for dropdown selection.")]
        public async Task<IActionResult> GetJobs(GetAllJobsQuery request) => Ok(await _mediator.Send(request));

        [Authorize]
        [HttpGet("shift-types")]
        [SwaggerResponse(200, "Success", typeof(Response<List<ShiftTypesDto>>))]
        [SwaggerOperation(Summary = "Get Shifts Catalog", Description = "Returns a list of all available work shifts (Morning, Afternoon, Evening, Night) for dropdown selection.")]
        public async Task<IActionResult> GetShifts(GetAllShiftsQuery request) => Ok(await _mediator.Send(request));

        [Authorize(Roles = "Worker")]
        [HttpPost("submit")]
        [SwaggerResponse(200, "Success", typeof(Response<int>))]
        [SwaggerOperation(Summary = "Submit Schedule Request", Description = "Allows a worker to submit a schedule request specifying Job, Date, and Shift.")]
        public async Task<IActionResult> SubmitSchedule(SubmitScheduleRequestCommand request) => Ok(await _mediator.Send(request));

        [Authorize(Roles = "Worker")]
        [HttpGet("my-schedules")]
        [SwaggerResponse(200, "Success", typeof(Response<List<ScheduleDto>>))]
        [SwaggerOperation(Summary = "Get User Schedules", Description = "Returns all schedules submitted by the currently logged-in worker.")]
        public async Task<IActionResult> GetUserSchedules([FromQuery] GetUserSchedulesQuery request) => Ok(await _mediator.Send(request));

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        [SwaggerResponse(200, "Success", typeof(Response<List<ScheduleDto>>))]
        [SwaggerOperation(Summary = "Get All Schedules", Description = "Returns all existing schedules. Admin access only.")]
        public async Task<IActionResult> GetAllSchedules(GetAllSchedulesQuery request) => Ok(await _mediator.Send(request));

        [Authorize(Roles = "Admin")]
        [HttpPut("approve")]
        [SwaggerResponse(200, "Success", typeof(Response<bool>))]
        [SwaggerOperation(Summary = "Approve Schedule", Description = "Approves a pending schedule request.")]
        public async Task<IActionResult> ApproveSchedule(ApproveScheduleCommand request) => Ok(await _mediator.Send(request));

        [Authorize(Roles = "Admin")]
        [HttpPut("reject")]
        [SwaggerResponse(200, "Success", typeof(Response<bool>))]
        [SwaggerOperation(Summary = "Reject Schedule", Description = "Rejects a pending schedule request.")]
        public async Task<IActionResult> RejectSchedule(RejectScheduleCommand request) => Ok(await _mediator.Send(request));


    }
}

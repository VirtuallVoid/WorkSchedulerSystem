using Application.DTOs.Auth.Responses;
using Application.Features.Auth.Commands.LoginUser;
using Application.Features.Auth.Commands.RegisterUser;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        [SwaggerResponse(200, "Success", typeof(Response<string>))]
        [SwaggerOperation(Summary = "Register User", Description = "Creates a new user with FullName, Username, Password, and RoleId. Use RoleId = 1 for Admin and RoleId = 2 for Worker.")]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand request) => Ok(await _mediator.Send(request));

        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerResponse(200, "Success", typeof(Response<AuthResponseDto>))]
        [SwaggerOperation(Summary = "Authorize User", Description = "Authenticates the user with Username and password, returning an access token (JWT) and basic account information.")]
        public async Task<IActionResult> LoginUser(LoginUserCommand request) => Ok(await _mediator.Send(request));
    }
}

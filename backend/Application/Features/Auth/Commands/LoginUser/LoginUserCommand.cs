using Application.DTOs.Auth.Responses;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<Response<AuthResponseDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

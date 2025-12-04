using Application.DTOs.Auth.Responses;
using Application.DTOs.Logging.Requests;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Wrappers;
using MediatR;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<AuthResponseDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IAuthService _auth;

        public LoginUserCommandHandler(IUnitOfWork uow, IAuthService auth)
        {
            _uow = uow;
            _auth = auth;
        }

        public async Task<Response<AuthResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _uow.AuthRepository.ValidateUserByUserName(request.UserName);
                if (user == null)
                    throw new AuthException("Invalid credentials", "INVALID_CREDENTIALS");

                var isValidPassword = HashHelper.VerifyPassword(request.Password, user.PasswordHash);
                if (!isValidPassword)
                    throw new AuthException("Invalid credentials", "INVALID_CREDENTIALS");

                var AuthResponse = _auth.GenerateAccessToken(user);

                return Response<AuthResponseDto>.Success(AuthResponse, 200, "Login successful");
            }
            catch (AppException) { throw; }
            catch (Exception ex)
            {
                await ExceptionHelper.HandleExceptionAsync(
                    new LogRequestDto("LoginUser", "Login failed")
                    {
                        Details = $"UserName: {request.UserName}"
                    },
                    ex,
                    _uow.LoggingRepository
                );

                throw;
            }
        }
    }
}

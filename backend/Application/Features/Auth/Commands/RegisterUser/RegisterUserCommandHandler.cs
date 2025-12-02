using Application.DTOs.Logs;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, Response<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _maper;

        public RegisterUserCommandHandler(IUnitOfWork uow, IMapper maper)
        {
            _uow = uow;
            _maper = maper;
        }

        public async Task<Response<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _uow.AuthRepository.CheckIfUserExists(request.UserName);
                if (existingUser)
                    throw new AuthException("Username already taken", "USERNAME_EXISTS");

                var hashed = HashHelper.HashPassword(request.Password);
                var user = _maper.Map<User>(request);
                user.PasswordHash = hashed;
                var userId = await _uow.AuthRepository.CreateUserAsync(user);
                return Response<int>.Success(userId, 200, "User registered successfully");
            }
            catch (AppException) { throw; }
            catch (Exception ex)
            {
                await ExceptionHelper.HandleExceptionAsync(
                    new LogRequestDto("RegisterUser", "Registration failed")
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

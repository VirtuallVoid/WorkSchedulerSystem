using Application.DTOs.Auth.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<User> ValidateUserByUserName(string username);
        Task<bool> CheckIfUserExists(string username);
        Task<int> CreateUserAsync(User user);
        Task<UserDto> GetUserInfoByUserId(int userId);
    }
}

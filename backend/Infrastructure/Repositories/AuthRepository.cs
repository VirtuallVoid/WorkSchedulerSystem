using Application.DTOs.Auth.Responses;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Dapper;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthRepository(IDatabaseConfig dbConfig) : IAuthRepository
    {
        public async Task<User> ValidateUserByUserName(string username)
        {
            const string query = "EXEC [dbo].[spValidateUserByUserName] @UserName";

            await using var conn = new SqlConnection(dbConfig.ConnectionString);
            var user = await conn.QueryFirstOrDefaultAsync<User>(query, new { UserName = username });
            return user;
        }

        public async Task<bool> CheckIfUserExists(string username)
        {
            const string query = "EXEC [dbo].[spCheckIfUserExists] @UserName";

            await using var conn = new SqlConnection(dbConfig.ConnectionString);
            var user = await conn.QueryFirstOrDefaultAsync<bool>(query, new { UserName = username });
            return user;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            const string sql = "[dbo].[spCreateUser]";
            await using var conn = new SqlConnection(dbConfig.ConnectionString);
            var newUserId = await conn.QueryFirstOrDefaultAsync<int>(sql,
                new
                {
                    user.FullName,
                    user.Username,
                    user.PasswordHash,
                    user.RoleId,
                    user.JobId
                }
            );

            return newUserId;
        }

        public async Task<UserDto> GetUserInfoByUserId(int userId)
        {
            const string query = "EXEC [dbo].[spGetUserInfoByUserId] @UserId";

            await using var conn = new SqlConnection(dbConfig.ConnectionString);
            var user = await conn.QueryFirstOrDefaultAsync<UserDto>(query, new { UserId = userId });
            return user;
        }
    }
}

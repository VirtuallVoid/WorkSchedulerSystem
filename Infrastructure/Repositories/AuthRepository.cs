using Application.Exceptions;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Dapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthRepository(IConnectionFactory connectionFactory) : IAuthRepository
    {
        public async Task<User> ValidateUserByUserName(string username)
        {
            const string query = "EXEC [dbo].[spValidateUserByUserName] @UserName";

            using var conn = connectionFactory.CreateConnection();
            var user = await conn.QueryFirstOrDefaultAsync<User>(query, new { UserName = username });
            return user;
        }

        public async Task<bool> CheckIfUserExists(string username)
        {
            const string query = "EXEC [dbo].[spCheckIfUserExists] @UserName";

            using var conn = connectionFactory.CreateConnection();
            var user = await conn.QueryFirstOrDefaultAsync<bool>(query, new { UserName = username });
            return user;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            const string sql = "[dbo].[spCreateUser]";
            using var conn = connectionFactory.CreateConnection();
            var newUserId = await conn.QueryFirstOrDefaultAsync<int>(sql,
                new
                {
                    user.FullName,
                    user.Username,
                    user.PasswordHash,
                    user.RoleId
                }
            );

            return newUserId;
        }
    }
}

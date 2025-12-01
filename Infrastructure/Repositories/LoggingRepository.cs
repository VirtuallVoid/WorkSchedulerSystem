using Application.DTOs.Logs;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LoggingRepository(IConnectionFactory connectionFactory) : ILoggingRepository
    {
        public async Task InsertLogAsync(LogRequestDto log)
        {
            string query = "EXEC [log].[spInsertLogEntry] @OperationName, @Message, @Exception, @Details";

            using var conn = connectionFactory.CreateConnection();
            await conn.ExecuteAsync(query, new
            {
                log.OperationName,
                log.Message,
                log.Exception,
                log.Details
            });
        }
    }
}

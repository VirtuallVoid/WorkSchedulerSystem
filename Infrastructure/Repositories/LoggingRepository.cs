using Application.DTOs.Logs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LoggingRepository(IDatabaseConfig dbConfig) : ILoggingRepository
    {
        public async Task InsertLogAsync(LogRequestDto log)
        {
            string query = "EXEC [log].[spInsertLogEntry] @OperationName, @Message, @Exception, @Details";

            await using var conn = new SqlConnection(dbConfig.ConnectionString);
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

using Application.Interfaces.Factories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionFactory;

        public ConnectionFactory(IConfiguration configuration)
        {
            _connectionFactory = configuration.GetConnectionString("dbConnection");
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionFactory);
            return connection;
        }
    }
}

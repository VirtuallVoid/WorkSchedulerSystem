using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DatabaseConfig(string ConnectionString) : IDatabaseConfig
    {
        public string ConnectionString { get; set; } = ConnectionString;
    }
}

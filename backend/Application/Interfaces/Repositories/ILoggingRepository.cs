using Application.DTOs.Logging.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ILoggingRepository
    {
        Task InsertLogAsync(LogRequestDto log);
    }
}

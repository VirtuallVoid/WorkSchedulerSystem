using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        ILoggingRepository LoggingRepository { get; }
        IAuthRepository AuthRepository { get; }
    }
}

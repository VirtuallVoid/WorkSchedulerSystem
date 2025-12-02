using Application.Interfaces;
using Application.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork(IServiceProvider serviceProvider) : IUnitOfWork
    {
        public ILoggingRepository LoggingRepository => serviceProvider.GetService<ILoggingRepository>()!;
        public IAuthRepository AuthRepository => serviceProvider.GetService<IAuthRepository>()!;
        public ISchedulesRepository SchedulesRepository => serviceProvider.GetService<ISchedulesRepository>()!;
    }
}

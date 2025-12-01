using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message, string errorCode = "NOT_FOUND")
            : base(message, errorCode)
        {
        }

        public NotFoundException(string message, Exception innerException, string errorCode = "NOT_FOUND")
            : base(message, innerException, errorCode)
        {
        }
    }
}

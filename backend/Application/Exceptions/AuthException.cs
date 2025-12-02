using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class AuthException : AppException
    {
        public AuthException(string message, string errorCode = "AUTH_ERROR")
            : base(message, errorCode)
        {
        }

        public AuthException(string message, Exception innerException, string errorCode = "AUTH_ERROR")
            : base(message, innerException, errorCode)
        {
        }
    }
}

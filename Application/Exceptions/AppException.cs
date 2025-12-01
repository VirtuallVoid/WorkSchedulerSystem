using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public abstract class AppException : Exception
    {
        public string ErrorCode { get; }
        public object DataObject { get; }

        protected AppException(string message, string errorCode = "", object data = null)
            : base(message)
        {
            ErrorCode = errorCode;
            DataObject = data;
        }

        protected AppException(string message, Exception innerException, string errorCode = "", object data = null)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            DataObject = data;
        }
    }
}

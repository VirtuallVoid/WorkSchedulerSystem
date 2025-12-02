using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Exceptions
{
    public class ValidationException : AppException
    {
        public IReadOnlyCollection<ValidationFailure> Failures { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base("Validation failed", "VALIDATION_ERROR")
        {
            Failures = failures?.ToList().AsReadOnly()
                ?? new List<ValidationFailure>().AsReadOnly();
        }

        public ValidationException(string message, Exception innerException, string errorCode = "VALIDATION_ERROR")
            : base(message, innerException, errorCode)
        {
            Failures = new List<ValidationFailure>().AsReadOnly();
        }

        public ValidationException(string message, string errorCode = "VALIDATION_ERROR")
            : base(message, errorCode)
        {
            Failures = new List<ValidationFailure>().AsReadOnly();
        }
    }
}

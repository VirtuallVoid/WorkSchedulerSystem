using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Logging.Requests
{
    public record LogRequestDto(string OperationName, string Message)
    {
        public string Exception { get; set; }
        public string Details { get; set; }
    }
}

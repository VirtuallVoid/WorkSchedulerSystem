using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }
        public IDictionary<string, string[]> ValidationErrors { get; set; } = new Dictionary<string, string[]>();

        public Response() { }

        public Response(T data, int code, string message = null)
        {
            Succeeded = true;
            Message = message;
            Code = code;
            Data = data;
        }

        public Response(string message, int code = 400, IDictionary<string, string[]> validationErrors = null)
        {
            Succeeded = false;
            Message = message;
            Code = code;
            ValidationErrors = validationErrors ?? new Dictionary<string, string[]>();
        }

        public static Response<T> Success(T data, int code, string message) =>
            new Response<T>(data, code, message);

        public static Response<T> Fail(int code, string message, string error = null, IDictionary<string, string[]> validationErrors = null) =>
            new Response<T>(default, code, message)
            {
                Succeeded = false,
                Error = error,
                ValidationErrors = validationErrors ?? new Dictionary<string, string[]>()
            };
    }
}

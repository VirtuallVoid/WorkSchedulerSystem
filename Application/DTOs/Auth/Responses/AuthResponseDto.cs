using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Responses
{
    public class AuthResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string BearerToken { get; set; }
        public DateTime TokenExpirationDate { get; set; }
    }
}

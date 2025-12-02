using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _context;

        public UserContext(IHttpContextAccessor context)
        {
            _context = context;
        }

        public int UserId
        {
            get
            {
                var claim = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(claim))
                    throw new AuthException("UserId claim missing", "INVALID_TOKEN");
                return int.Parse(claim);
            }
        }

        public string UserName
        {
            get
            {
                return _context.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown";
            }
        }

        public string RoleName
        {
            get
            {
                var role = _context.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
                if (string.IsNullOrEmpty(role))
                    role = _context.HttpContext?.User?.FindFirst("role")?.Value;
                return role ?? "Unknown";
            }
        }
    }
}

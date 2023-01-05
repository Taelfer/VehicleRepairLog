using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace VehicleRepairLog.Application.Authentication
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetCurrentUserId()
        {
            int? userId = null;
            string identity = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (identity is not null)
            {
                userId = Int32.Parse(identity);
                return userId;
            }
            return null;
        }

        public string GetCurrentUserRole()
        {
            string userRole = "";
            string identity = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (identity is not null)
            {
                userRole = identity;
            }
            return null;
        }
    }
}

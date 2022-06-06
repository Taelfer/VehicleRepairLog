using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using VehicleRepairLog.Application.Models;

namespace VehicleRepairLog.Application.Authentication
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserDto GetCurrentUser()
        {
            ClaimsPrincipal identity = _httpContextAccessor.HttpContext.User;

            if (identity is not null)
            {
                var userClaims = identity.Claims;

                return new UserDto
                {
                    Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}

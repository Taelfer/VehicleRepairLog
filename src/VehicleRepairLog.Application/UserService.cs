using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using VehicleRepairLog.Application.Models;

namespace VehicleRepairLog.Application
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public UserViewDto GetCurrentUser()
        {
            var identity = httpContextAccessor.HttpContext.User as ClaimsPrincipal;

            if (identity is not null)
            {
                var userClaims = identity.Claims;

                return new UserViewDto
                {
                    Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}

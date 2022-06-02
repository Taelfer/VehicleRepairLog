﻿using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using VehicleRepairLog.Application.Models;

namespace VehicleRepairLog.Application.Authentication
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public UserDto GetCurrentUser()
        {
            var identity = httpContextAccessor.HttpContext.User as ClaimsPrincipal;

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

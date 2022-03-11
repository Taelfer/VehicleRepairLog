using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Users;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Users
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;
        private readonly IPasswordHasher<User> passwordHasher;

        public LoginUserHandler(IMapper mapper, IQueryExecutor queryExecutor, IPasswordHasher<User> passwordHasher)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
            this.passwordHasher = passwordHasher;
        }

        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var query = new LoginUserQuery()
            {
                Username = request.Username
            };
            var user = await this.queryExecutor.Execute(query);

            if (user is null)
            {
                return new LoginUserResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var verifiedPassword = this.passwordHasher
                .VerifyHashedPassword(user, user.Password, request.Password);

            if (verifiedPassword is PasswordVerificationResult.Failed)
            {
                return new LoginUserResponse()
                {
                    Error = new ErrorModel(ErrorType.NotAuthenticated)
                };
            }



            throw new NotImplementedException();
        }
    }
}

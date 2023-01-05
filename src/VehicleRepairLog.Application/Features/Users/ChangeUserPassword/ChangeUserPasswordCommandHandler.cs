using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Users.ChangeUserPassword
{
    public class ChangeUserPasswordCommand : IRequest<PasswordChangeResultDto>
    {
        public int UserId;
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    internal class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, PasswordChangeResultDto>
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly VehicleProfileStorageContext _context;

        public ChangeUserPasswordCommandHandler(IMapper mapper, IPasswordHasher<User> passwordHasher, VehicleProfileStorageContext context)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _context = context;
        }

        public async Task<PasswordChangeResultDto> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.UserId);

            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }

            User updatedUser = _mapper.Map(request, user);

            string hashedPassword = _passwordHasher.HashPassword(user, request.NewPassword);
            updatedUser.Password = hashedPassword;

            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync();

            return new PasswordChangeResultDto
            {
                Successful = true
            };
        }
    }
}
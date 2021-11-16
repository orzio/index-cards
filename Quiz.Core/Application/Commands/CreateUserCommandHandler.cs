using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Quiz.Core.Domain.Auth;
using Quiz.Core.DTO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            var createdUser = await _userManager.CreateAsync(user, request.Password);

            if (!createdUser.Succeeded)
            {
                throw new Exception("Ivalid data");
            }

            return _mapper.Map<User, UserDto>(user);
        }
    }
}

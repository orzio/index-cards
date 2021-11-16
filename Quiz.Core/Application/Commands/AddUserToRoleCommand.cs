using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Quiz.Core.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class AddUserToRoleCommand:IRequest
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class AddUserToRoleCommandHandler : IRequestHandler<AddUserToRoleCommand, Unit>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public AddUserToRoleCommandHandler(RoleManager<Role> roleManager, UserManager<User> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.SingleOrDefault(user => user.UserName == request.Email);
            var result = await _userManager.AddToRoleAsync(user, request.Role);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return new Unit();
        }
    }
}

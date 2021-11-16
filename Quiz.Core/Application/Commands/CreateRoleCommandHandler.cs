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
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public CreateRoleCommandHandler(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role
            {
                Name = request.Name
            };

            var createdRole = await _roleManager.CreateAsync(role);

            if (!createdRole.Succeeded)
            {
                throw new Exception("Not created!");
            }

            return _mapper.Map<Role, RoleDto>(role);
        }
    }
}

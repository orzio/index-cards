using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quiz.Core.Domain.Auth;
using Quiz.Core.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, JwtToken>
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public LoginUserCommandHandler(UserManager<User> userManager, IMapper mapper, IOptionsSnapshot<JwtSettings> settings)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtSettings = settings.Value;
        }
        public async Task<JwtToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.SingleOrDefault(user => user.Email == request.Email);

            if (user is null)
                throw new Exception("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);

            var currentUser = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!currentUser)
                throw new Exception("Invalid credentials");

            var token = GenerateJwt(user, roles);

            JwtToken jwtToken = new JwtToken() { Token = token };

            return jwtToken;
        }

        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

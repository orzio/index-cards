using MediatR;
using Quiz.Core.DTO;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Core.Application.Commands
{
    public class LoginUserCommand:IRequest<JwtToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

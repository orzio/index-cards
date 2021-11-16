using MediatR;
using Quiz.Core.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz.Core.Application.Commands
{
    public class CreateUserCommand:IRequest<UserDto>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
    }
}

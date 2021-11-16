using MediatR;
using Quiz.Core.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz.Core.Application.Commands
{
    public class CreateRoleCommand:IRequest<RoleDto>
    {
        public string Name { get; set; }
    }
}

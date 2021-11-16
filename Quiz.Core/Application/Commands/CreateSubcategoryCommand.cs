using MediatR;
using Quiz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class CreateSubcategoryCommand:IRequest<CategoryDto>
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
}

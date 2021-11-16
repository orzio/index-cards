using MediatR;
using Quiz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class CreateMainCategoryCommand:IRequest<CategoryDto>
    {
        public string Name { get; set; }
    }
}

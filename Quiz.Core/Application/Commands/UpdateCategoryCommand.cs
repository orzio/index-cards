using MediatR;
using Quiz.Core.Domain;
using Quiz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class UpdateCategoryCommand:IRequest<CategoryDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<Question> Questions { get; set; }
    }
}

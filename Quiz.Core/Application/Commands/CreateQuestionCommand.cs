using MediatR;
using Quiz.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class CreateQuestionCommand:IRequest<QuestionDto>
    {
        public string Content { get; set; }
        public string Answer { get; set; }
        public int CategoryId { get; set; }
    }
}

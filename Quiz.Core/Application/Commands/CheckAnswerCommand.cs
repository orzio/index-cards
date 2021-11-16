using MediatR;
using Quiz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class CheckAnswerCommand:IRequest<AnswerResultDto>
    {
        public int CurrentQuestionId { get; set; }
        public int CurrentQuizId { get; set; }
        public string Answer { get; set; }
    }
}

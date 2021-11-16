using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz.Core.Application.Commands
{
    public class UpdateQuestionCommand:IRequest
    {
        public string QuestionContent { get; set; }
        public string AnswerContent { get; set; }
        public int Id { get; set; }
    }
}
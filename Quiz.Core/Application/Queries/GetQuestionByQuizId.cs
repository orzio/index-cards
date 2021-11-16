using MediatR;
using Quiz.Core.Domain;
using Quiz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Queries
{
    public class GetQuestionByQuizId:IRequest<QuestionWithQuizStatusDto>
    {
        public int QuizId { get; set; }
    }
}

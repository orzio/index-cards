using Quiz.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.DTO
{
    public class GetQuestionWithAnswerByIdDto
    {
        public QuestionDto Question { get; set; }
        public AnswerDto Answer { get; set; }
    }
}

using Quiz.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.DTO
{
    public class QuestionWithQuizStatusDto
    {
        public QuizStatusDto QuizStatusDto { get; set; }
        public QuestionDto QuestionDto { get; set; }
    }
}

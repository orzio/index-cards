using Quiz.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Repositories
{
    public interface IQuizRepository : IRepository<QuizRound>
    {
        Task AddQuiz(QuizRound quizRound);
        Task ChangeStatus(Status status, int quizId);
    }
}

using Quiz.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Repositories
{
    public interface IQuizRoundResultRepository:IRepository<QuizRoundResult>
    {
        Task InitializeQuizResults(IEnumerable<QuizRoundResult> quizRoundResults);
        Task<IEnumerable<QuizRoundResult>> GetAllResultsByQuizId(int quizId);
        Task IncreaseWeight(float weight, int quizId, int questionId);

    }
}

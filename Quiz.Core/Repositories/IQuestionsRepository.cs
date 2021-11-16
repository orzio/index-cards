using Quiz.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Repositories
{
   public interface IQuestionsRepository:IRepository<Question>
    {
        Task<Question> AddQuestionWithAnswer(string contenet, string answer, int categoryId);
        Task<IEnumerable<Question>> GetAllQuestionsWithCategory(int id);
        Task<Question> GetQuestionWithAnswerById(int questionId);
        Task<IEnumerable<Question>> GetAllQuestionsWithAnswersForCategory(int id);
    }
}

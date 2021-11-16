using Quiz.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Repositories
{
    public interface IAnswerRepository:IRepository<Answer>
    {
        Task<string> GetAnswerContentByQuestionId(int questionId);
        Task<Answer> GetAnswerByQuestionId(int questionId);
    }
}

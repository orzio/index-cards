using Microsoft.EntityFrameworkCore;
using Quiz.Core.Domain;
using Quiz.Core.Repositories;
using Quiz.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Infrastructure.Repositories
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(QuizDbContext context) : base(context) { }

        public async Task<string> GetAnswerContentByQuestionId(int questionId)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(x => x.QuestionId == questionId);
            return answer.Content;
        }

        public async Task<Answer> GetAnswerByQuestionId(int questionId)
            => await _context.Answers.FirstOrDefaultAsync(x => x.QuestionId == questionId);
    }
}
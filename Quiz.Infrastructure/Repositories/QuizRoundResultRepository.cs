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
    public class QuizRoundResultRepository : BaseRepository<QuizRoundResult>, IQuizRoundResultRepository
    {
        public QuizRoundResultRepository(QuizDbContext context) : base(context) { }

        public async Task InitializeQuizResults(IEnumerable<QuizRoundResult> quizRoundResults)
        {
            await _context.QuizRoundResults.AddRangeAsync(quizRoundResults);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<QuizRoundResult>> GetAllResultsByQuizId(int quizId) => await _context.QuizRoundResults.Where(x => x.QuizRoundId == quizId)
            .Where(x => x.Weight <= 1).ToListAsync();

        public async Task IncreaseWeight(float weight, int quizId, int questionId)
        {
            var result = await _context.QuizRoundResults.FirstOrDefaultAsync(res => res.QuestionId == questionId && res.QuizRoundId == quizId);
            result.Weight += weight;
            await _context.SaveChangesAsync();
        }
    }
}

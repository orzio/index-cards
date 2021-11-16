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
    public class QuizRepository : BaseRepository<QuizRound>, IQuizRepository
    {
        public QuizRepository(QuizDbContext context) : base(context)
        {
        }

        public async Task AddQuiz(QuizRound quizRound)
        {
            _context.Competitions.Add(quizRound);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeStatus(Status status, int quizId)
        {
            var quiz = await _context.Competitions.FirstOrDefaultAsync(quiz => quiz.Id == quizId);
            quiz.Status = (int)status;
            await _context.SaveChangesAsync();
        }
    }
}
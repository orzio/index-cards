using Microsoft.EntityFrameworkCore;
using Quiz.Core.Application.Commands;
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
    public class QuestionRepository : BaseRepository<Question>, IQuestionsRepository
    {
        public QuestionRepository(QuizDbContext context) : base(context)
        {
        }

        public async Task<Question> AddQuestionWithAnswer(string content, string answer, int categoryId)
        {
            var newQuestion = _context.Questions.Add(new Question() { Content = content, CategoryId = categoryId });
            await _context.SaveChangesAsync();
            _context.Answers.Add(new Answer() { Content = answer, QuestionId = newQuestion.Entity.Id });
            await _context.SaveChangesAsync();
            return newQuestion.Entity;
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsWithCategory(int id) => await _context.Questions.Where(x => x.CategoryId == id).ToListAsync();
        public async Task<IEnumerable<Question>> GetAllQuestionsWithAnswersForCategory(int id)
            => await _context.Questions.Where(x => x.CategoryId == id)
            .Include(question => question.Category)
            .Include(question => question.Answer)
            .ToListAsync();

        public async Task<Question> GetQuestionWithAnswerById(int questionId)
            => await _context.Questions.Include(question => question.Answer).FirstOrDefaultAsync(question => question.Id == questionId);

    }
}

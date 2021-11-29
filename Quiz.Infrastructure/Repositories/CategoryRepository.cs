using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quiz.Core.Domain;
using Quiz.Core.DTO;
using Quiz.Core.Repositories;
using Quiz.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(QuizDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetAllMainCategories() => await _context.Categories.Where(x => x.ParentCategoryId == null).ToListAsync();
        public async Task<IEnumerable<Category>> GetSubCategoriesForCategoryId(int id) => await _context.Categories.Where(x => x.ParentCategoryId == id).ToListAsync();
        public async Task<IEnumerable<CategoryWithChildrenDto>> GetAllCategoryWithChildren()
        {
            var mainCategories = await GetAllMainCategories();
            var mainCategoryDto = mainCategories.Select(x => new CategoryWithChildrenDto() { Id = x.Id, Name = x.Name, ParentCategoryId = x.ParentCategoryId }).ToList();

            foreach (var mainCategory in mainCategoryDto)
            {
                mainCategory.SubCategories.AddRange(await GetCategoryWithSubScategories(mainCategory.Id));
            }

            return mainCategoryDto;

        }

        public async Task<List<CategoryWithChildrenDto>> GetCategoryWithSubScategories(int id)
        {
            var categories = (await _context.Categories.Where(x => x.ParentCategoryId == id).ToListAsync()).Select(x => new CategoryWithChildrenDto()
            {
                Id = x.Id,
                Name = x.Name,
                ParentCategoryId = x.ParentCategoryId,
            }).ToList();
            foreach (var category in categories)
            {
                category.SubCategories.AddRange(await GetCategoryWithSubScategories(category.Id));
            }
            return categories;
        }


        public async Task<IEnumerable<CategoryWithChildrenDto>> GetMainCategoriesWithChildernContainingQuestions()
        {
            var mainCategories = await GetAllMainCategories();
            var mainCategoryDto = mainCategories.Select(x => new CategoryWithChildrenDto() { Id = x.Id, Name = x.Name, ParentCategoryId = x.ParentCategoryId }).ToList();

            foreach (var mainCategory in mainCategoryDto)
            {
                mainCategory.SubCategories.AddRange(await GetSubScategoriesWhichContainsChilderForSpecificCategory(mainCategory.Id));
            }

            mainCategoryDto = mainCategoryDto.Where(mainCategory => mainCategory.SubCategories.Any()).ToList();

            return mainCategoryDto;

        }

        public async Task<List<CategoryWithChildrenDto>> GetSubScategoriesWhichContainsChilderForSpecificCategory(int id)
        {
            var categories = (await _context.Categories.Where(x => x.ParentCategoryId == id)
                .Include(x => x.Questions)
                .Where(x => x.Questions.Any()).ToListAsync())
                .Select(x => new CategoryWithChildrenDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentCategoryId = x.ParentCategoryId,
                }).ToList();
            foreach (var category in categories)
            {
                category.SubCategories.AddRange(await GetSubScategoriesWhichContainsChilderForSpecificCategory(category.Id));
            }
            return categories;
        }


        private async Task<List<CategoryWithSubcategoriesContainingQuestionsAndAnswers>> GetSubScategoriesWhichContainsChilderForSpecificCategoryWithQuestionsAndAnswers(int id)
        {
            var categories = (await _context.Categories.Where(x => x.ParentCategoryId == id)
                .Include(x => x.Questions).ThenInclude(x => x.Answer).AsNoTracking()
                .Select(x => new CategoryWithSubcategoriesContainingQuestionsAndAnswers()
                {
                    Category = new()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ParentCategoryId = x.ParentCategoryId,
                        Questions = x.Questions
                    },
                }).ToListAsync());

            var flatenCategories = new List<CategoryWithSubcategoriesContainingQuestionsAndAnswers>(categories);
            foreach (var category in categories)
            {
                flatenCategories.AddRange(await GetSubScategoriesWhichContainsChilderForSpecificCategoryWithQuestionsAndAnswers(category.Category.Id));
            }
            return flatenCategories;
        }


        public async Task<Category> CreateCategory(string name)
        {
            var newCategory = new Category() { Name = name };
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }

        public async Task DeleteCascadeAsync(Category categoryToDelete)
        {
            var categories = await GetSubScategoriesWhichContainsChilderForSpecificCategoryWithQuestionsAndAnswers(categoryToDelete.Id);

            foreach (var category in categories)
            {
                await DeleteAllQuestionAndAnswersForGivenCategory(category);
                await DeleteGivenCategory(category);
            }
            await DeleteGivenCategory(categoryToDelete);
        }

        private async Task DeleteAllQuestionAndAnswersForGivenCategory(CategoryWithSubcategoriesContainingQuestionsAndAnswers category)
        {
            var answers = category.Category.Questions.Select(question => question.Answer);
            await DeleteAllAnswersForGivenQuestion(answers);
            var questions = category.Category.Questions;

            _context.Questions.RemoveRange(questions);
            await _context.SaveChangesAsync();
        }

        private async Task DeleteAllAnswersForGivenQuestion(IEnumerable<Answer> answers)
        {
            _context.Answers.RemoveRange(answers);
            await _context.SaveChangesAsync();
        }

        private async Task DeleteGivenCategory(CategoryWithSubcategoriesContainingQuestionsAndAnswers category)
        {
            try
            {
               var categoryToDelete = await _context.Categories.FindAsync(category.Category.Id);
            _context.Categories.Remove(categoryToDelete);
            }catch(Exception ex)
            {
                var mess = ex.Message;
            }
            await _context.SaveChangesAsync();
        }

        private async Task DeleteGivenCategory(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

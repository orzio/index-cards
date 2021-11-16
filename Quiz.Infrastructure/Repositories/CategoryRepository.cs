using Microsoft.EntityFrameworkCore;
using Quiz.Core.Domain;
using Quiz.Core.DTO;
using Quiz.Core.Repositories;
using Quiz.Infrastructure.EF;
using System;
using System.Collections.Generic;
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

        public async Task<Category> CreateCategory(string name)
        {
            var newCategory = new Category() { Name = name };
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }
    }
}

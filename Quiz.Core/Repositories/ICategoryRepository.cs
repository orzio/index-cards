using Quiz.Core.Domain;
using Quiz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllMainCategories();
        Task<IEnumerable<Category>> GetSubCategoriesForCategoryId(int id);
        Task<IEnumerable<CategoryWithChildrenDto>> GetAllCategoryWithChildren();
        Task<IEnumerable<CategoryWithChildrenDto>> GetMainCategoriesWithChildernContainingQuestions();
        Task<Category> CreateCategory(string name);
    }
}

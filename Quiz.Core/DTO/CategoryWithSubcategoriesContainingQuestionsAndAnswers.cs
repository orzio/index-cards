using Quiz.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.DTO
{
    public class CategoryWithSubcategoriesContainingQuestionsAndAnswers
    {
        public Category Category { get; set; }
        public List<CategoryWithSubcategoriesContainingQuestionsAndAnswers> SubCategories { get; set; } = new();
    }
}

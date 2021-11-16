using MediatR;
using Quiz.Core.DTO;
using Quiz.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Queries
{
    public class GetAllSubcategoriesQueryHandler : IRequestHandler<GetAllSubcategoriesQuery, IEnumerable<CategoryWithChildrenDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllSubcategoriesQueryHandler(ICategoryRepository questionRepository)
        {
            _categoryRepository = questionRepository;
        }

        public async Task<IEnumerable<CategoryWithChildrenDto>> Handle(GetAllSubcategoriesQuery request, CancellationToken cancellationToken)
        {
            return (await _categoryRepository.GetSubCategoriesForCategoryId(request.Id)).Select(mainCategory => new CategoryWithChildrenDto() { Id = mainCategory.Id, Name = mainCategory.Name, ParentCategoryId = mainCategory.ParentCategoryId }); ;
            //var mainCategories = await _categoryRepository.GetAllMainCategories();
            //return mainCategories
        }
    }
}

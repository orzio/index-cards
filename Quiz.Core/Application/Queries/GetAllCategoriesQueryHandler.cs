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
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategories, IEnumerable<CategoryWithChildrenDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoriesQueryHandler(ICategoryRepository questionRepository)
        {
            _categoryRepository = questionRepository;
        }
        public async Task<IEnumerable<CategoryWithChildrenDto>> Handle(GetAllCategories request, CancellationToken cancellationToken)
            =>await _categoryRepository.GetAllCategoryWithChildren();
    }
}

using MediatR;
using Quiz.Core.DTO;
using Quiz.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Queries
{
    public class GetAllCategoriesForQuizQueryHandler : IRequestHandler<GetAllCategoriesForQuiz, IEnumerable<CategoryWithChildrenDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoriesForQuizQueryHandler(ICategoryRepository questionRepository)
        {
            _categoryRepository = questionRepository;
        }
        public async Task<IEnumerable<CategoryWithChildrenDto>> Handle(GetAllCategoriesForQuiz request, CancellationToken cancellationToken)
            => await _categoryRepository.GetMainCategoriesWithChildernContainingQuestions();

    }
}

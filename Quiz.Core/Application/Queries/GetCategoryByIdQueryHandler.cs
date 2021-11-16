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
    public class GetCategoryByIdQueryHandler:IRequestHandler<GetCategoryById, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<CategoryDto> Handle(GetCategoryById request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        return new CategoryDto { Id = category.Id, Name = category.Name, ParentCategoryId = category.ParentCategoryId };
    }
}
}

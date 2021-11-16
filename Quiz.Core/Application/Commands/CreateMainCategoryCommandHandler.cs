using MediatR;
using Quiz.Core.DTO;
using Quiz.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class CreateMainCategoryCommandHandler : IRequestHandler<CreateMainCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateMainCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryDto> Handle(CreateMainCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.CreateCategory(request.Name);
            return new CategoryDto {Id = category.Id, Name = category.Name };
        }
    }
}

using MediatR;
using Quiz.Core.Domain;
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
    public class CreateSubcategoryCommandHandler : IRequestHandler<CreateSubcategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateSubcategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryDto> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category() { Name = request.Name, ParentCategoryId = request.ParentId};
            await _categoryRepository.AddAsync(category);
            return new CategoryDto() { Id = category.Id, Name = category.Name, ParentCategoryId = category.ParentCategoryId };
        }
    }
}

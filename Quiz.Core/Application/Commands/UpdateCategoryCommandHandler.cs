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
   public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category() { Id = request.Id, Name = request.Name, ParentCategoryId = request.ParentCategoryId, Questions = request.Questions };
            await _categoryRepository.UpdateAsync(category);
            return new CategoryDto();
        }
    }
}

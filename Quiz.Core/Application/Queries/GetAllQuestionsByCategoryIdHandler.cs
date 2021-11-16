using MediatR;
using Quiz.Core.Domain;
using Quiz.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Queries
{
    class GetAllQuestionsByCategoryIdHandler : IRequestHandler<GetAllQuestionsByCategoryId, IEnumerable<QuestionDto>>
    {
        private readonly IQuestionsRepository _questionsRepository;
        public GetAllQuestionsByCategoryIdHandler(IQuestionsRepository questionRepository)
        {
            _questionsRepository = questionRepository;
        }

        public async Task<IEnumerable<QuestionDto>> Handle(GetAllQuestionsByCategoryId request, CancellationToken cancellationToken)
        {
            var result = await _questionsRepository.GetAllQuestionsWithCategory(request.CategoryId);
            var resultDtos = result.Select(x => new QuestionDto() { CategoryId = x.CategoryId, Content = x.Content, Id = x.Id });
            return resultDtos;
        }
    }
}
using AutoMapper;
using MediatR;
using Quiz.Core.Application.Responses;
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
    public class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, IEnumerable<QuestionDto>>
    {
        private readonly IQuestionsRepository _questionsRepository;
        public GetAllQuestionsQueryHandler(IQuestionsRepository questionRepository)
        {
            _questionsRepository = questionRepository;
        }

        public async Task<IEnumerable<QuestionDto>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            var result = await _questionsRepository.GetAllAsync();
            var resultDtos = result.Select(x => new QuestionDto() { CategoryId = x.CategoryId, Content = x.Content, Id = x.Id });
            return resultDtos;
        }
    }
}

using AutoMapper;
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

namespace Quiz.Core.Application.Queries
{
    public class GetAllQuestionsWithAnswerForCategoryQueryHandler : IRequestHandler<GetAllQuestionsWithAnswerForCategory, IEnumerable<QuestionWithAnswerForCategoryDto>>
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IMapper _mapper;
        public GetAllQuestionsWithAnswerForCategoryQueryHandler(IQuestionsRepository questionRepository, IMapper mapper)
        {
            _questionsRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuestionWithAnswerForCategoryDto>> Handle(GetAllQuestionsWithAnswerForCategory request, CancellationToken cancellationToken)
        {
            var result = await _questionsRepository.GetAllQuestionsWithAnswersForCategory(request.Id);
            List<QuestionWithAnswerForCategoryDto> questionWithAnswer = new();

            foreach(var question in result)
            {
                questionWithAnswer.Add(new()
                {
                    Answer = new AnswerDto()
                    {
                        Content = question.Answer.Content,
                        Id = question.Answer.Id
                    },
                    Question = new QuestionDto()
                    {
                        CategoryId = question.CategoryId,
                        Content = question.Content,
                        Id = question.Id
                    }
                });
            }
            
            return questionWithAnswer;
        }
    }
}
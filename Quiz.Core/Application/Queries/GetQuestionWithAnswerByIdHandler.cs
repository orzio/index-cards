using MediatR;
using Quiz.Core.Domain;
using Quiz.Core.DTO;
using Quiz.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Queries
{
    public class GetQuestionWithAnswerByIdHandler : IRequestHandler<GetQuestionWithAnswerById, GetQuestionWithAnswerByIdDto>
    {
        private readonly IQuestionsRepository _questionsRepository;
        public GetQuestionWithAnswerByIdHandler(IQuestionsRepository questionRepository)
        {
            _questionsRepository = questionRepository;
        }

        public async Task<GetQuestionWithAnswerByIdDto> Handle(GetQuestionWithAnswerById request, CancellationToken cancellationToken)
        {
            var result = await _questionsRepository.GetQuestionWithAnswerById(request.QuestionId);
            QuestionWithAnswerForCategoryDto questionWithAnswer = new();

            return new()
            {
                Answer = new AnswerDto()
                {
                    Content = result.Answer.Content,
                    Id = result.Answer.Id
                },
                Question = new QuestionDto()
                {
                    CategoryId = result.CategoryId,
                    Content = result.Content,
                    Id = result.Id
                }
            };
        }

    }
}

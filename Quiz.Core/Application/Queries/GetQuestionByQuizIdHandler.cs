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
    public class GetQuestionByQuizIdHandler : IRequestHandler<GetQuestionByQuizId, QuestionWithQuizStatusDto>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IQuizRoundResultRepository _quizRoundResultRepository;
        public GetQuestionByQuizIdHandler(IQuestionsRepository questionRepository, IQuizRoundResultRepository quizRoundResultRepository, IQuizRepository quizRepository)
        {
            _questionsRepository = questionRepository;
            _quizRoundResultRepository = quizRoundResultRepository;
            _quizRepository = quizRepository;
        }

        public async Task<QuestionWithQuizStatusDto> Handle(GetQuestionByQuizId request, CancellationToken cancellationToken)
        {
            var status = await CheckQuizStatus(request.QuizId);

            if (status == Status.NotStarted)
                await SetStatus(Status.Active, request.QuizId);

            var quizRoundResults = await _quizRoundResultRepository.GetAllResultsByQuizId(request.QuizId);

            if (!quizRoundResults.Any())
            {
                await SetStatus(Status.Finished, request.QuizId);
                return new QuestionWithQuizStatusDto()
                {
                    QuestionDto = new() { },
                    QuizStatusDto = new() { Id = request.QuizId, Status = (int)Status.Finished }
                };
            }

            var questionIds = quizRoundResults.Select(x => x.QuestionId).ToList();
            var questionAmount = questionIds.Count();

            Random random = new Random();
            var index = random.Next(questionAmount);
            var questionId = questionIds[index];

            var question = await _questionsRepository.GetByIdAsync(questionId);

            status = await CheckQuizStatus(request.QuizId);

            var resultDto = new QuestionWithQuizStatusDto()
            {
                QuestionDto = new QuestionDto()
                {
                    CategoryId = question.CategoryId,
                    Content = question.Content,
                    Id = question.Id
                },
                QuizStatusDto = new QuizStatusDto() { Id = request.QuizId, Status = (int)status }

            };
            return resultDto;
        }
        private async Task<Status> CheckQuizStatus(int quizId)
        {
            var result = await _quizRepository.GetByIdAsync(quizId);
            return (Status)result.Status;
        }

        private async Task SetStatus(Status status, int quizId)
        {
            await _quizRepository.ChangeStatus(status, quizId);
        }
    }
}

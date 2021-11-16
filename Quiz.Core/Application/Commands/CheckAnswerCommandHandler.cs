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
    public class CheckAnswerCommandHandler : IRequestHandler<CheckAnswerCommand, AnswerResultDto>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizRoundResultRepository _quizRoundResultRepository;
        private readonly IQuestionsRepository _questionsRepository;
        public CheckAnswerCommandHandler(IQuizRepository quizRepository, IQuizRoundResultRepository quizRoundResultRepository, IQuestionsRepository questionsRepository)
        {
            _quizRepository = quizRepository;
            _quizRoundResultRepository = quizRoundResultRepository;
            _questionsRepository = questionsRepository;
        }
        public async Task<AnswerResultDto> Handle(CheckAnswerCommand request, CancellationToken cancellationToken)
        {
            var quizRound = await _quizRoundResultRepository.GetByIdAsync(request.CurrentQuizId);

            var question = await _questionsRepository.GetQuestionWithAnswerById(request.CurrentQuestionId);
            if (question == null)
                throw new Exception();

            bool isAnswerCorrect = question.Answer.Content == request.Answer;
            if (isAnswerCorrect)
            {
                await _quizRoundResultRepository.IncreaseWeight(0.05f, request.CurrentQuizId, request.CurrentQuestionId);
            }

            return new AnswerResultDto() { Result = isAnswerCorrect };
        }
    }
}

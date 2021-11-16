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
    public class CreateNewQuizCommandHandler : IRequestHandler<CreateNewQuizCommand, QuizDto>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizRoundResultRepository _quizRoundResultRepository;
        private readonly IQuestionsRepository _questionsRepository;
        public CreateNewQuizCommandHandler(IQuizRepository quizRepository, IQuizRoundResultRepository quizRoundResultRepository, IQuestionsRepository questionsRepository)
        {
            _quizRepository = quizRepository;
            _quizRoundResultRepository = quizRoundResultRepository;
            _questionsRepository = questionsRepository;
        }
        public async Task<QuizDto> Handle(CreateNewQuizCommand request, CancellationToken cancellationToken)
        {
            var quizRound = new QuizRound() { Status = (int)Status.NotStarted, QuizStart = DateTime.UtcNow };
            await _quizRepository.AddQuiz(quizRound);

            var questions = await _questionsRepository.GetAllQuestionsWithCategory(request.CategoryId);
            IEnumerable<QuizRoundResult> quizRoundResults = questions.Select(question => new QuizRoundResult()
            {
                QuestionId = question.Id,
                QuizRoundId = quizRound.Id,
                Weight = 0.8f
            });

            await _quizRoundResultRepository.InitializeQuizResults(quizRoundResults);

            return new QuizDto { Id = quizRound.Id };
        }
    }
}

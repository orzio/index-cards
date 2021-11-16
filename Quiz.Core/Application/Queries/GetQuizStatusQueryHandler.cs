using MediatR;
using Quiz.Core.DTO;
using Quiz.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Queries
{
    public class GetQuizStatusQueryHandler : IRequestHandler<GetQuizStatus, QuizStatusDto>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizRoundResultRepository _quizRoundResultRepository;
        public GetQuizStatusQueryHandler(IQuizRepository quizRepository, IQuizRoundResultRepository quizRoundResultRepository)
        {
            _quizRepository = quizRepository;
            _quizRoundResultRepository = quizRoundResultRepository;
        }
        public async Task<QuizStatusDto> Handle(GetQuizStatus request, CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetByIdAsync(request.QuizId);
            return new QuizStatusDto { Id = quiz.Id, Status = quiz.Status };
        }
    }
}


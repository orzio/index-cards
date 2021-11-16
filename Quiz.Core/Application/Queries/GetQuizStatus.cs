using MediatR;
using Quiz.Core.DTO;

namespace Quiz.Core.Application.Queries
{
    public class GetQuizStatus : IRequest<QuizStatusDto>
    {
        public int QuizId { get; set; }
    }
}


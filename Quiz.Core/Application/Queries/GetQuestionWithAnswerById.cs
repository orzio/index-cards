using MediatR;
using Quiz.Core.DTO;

namespace Quiz.Core.Application.Queries
{
    public class GetQuestionWithAnswerById : IRequest<GetQuestionWithAnswerByIdDto>
    {
        public int QuestionId { get; set; }
    }
}

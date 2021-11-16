using MediatR;
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
    public class GetAnswerByQuestionIdHandler : IRequestHandler<GetAnswerByQuestionId, AnswerDto>
    {
        private readonly IAnswerRepository _answersRepository;
        public GetAnswerByQuestionIdHandler(IAnswerRepository answersRepository)
        {
            _answersRepository = answersRepository;
        }

        public async Task<AnswerDto> Handle(GetAnswerByQuestionId request, CancellationToken cancellationToken)
        {
            var result = await _answersRepository.GetAnswerContentByQuestionId(request.QuestionId);
            return new AnswerDto() { Content = result};
        }
    }
}
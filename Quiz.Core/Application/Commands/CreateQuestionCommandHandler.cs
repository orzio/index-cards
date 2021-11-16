using MediatR;
using Quiz.Core.Domain;
using Quiz.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class CreateQuestionCommandHandler:IRequestHandler<CreateQuestionCommand, QuestionDto>
    {
        private readonly IQuestionsRepository _questionsRepository;
        public CreateQuestionCommandHandler(IQuestionsRepository questionRepository)
        {
            _questionsRepository = questionRepository;
        }
        public async Task<QuestionDto> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionsRepository.AddQuestionWithAnswer(request.Content, request.Answer, request.CategoryId);
            return new QuestionDto { CategoryId = question.CategoryId, Content = question.Content, Id = question.Id };
        }
    }
}

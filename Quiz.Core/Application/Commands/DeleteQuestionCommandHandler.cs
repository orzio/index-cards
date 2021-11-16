using MediatR;
using Quiz.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class DeleteQuestionCommandHandler:IRequestHandler<DeleteQuestionCommand, Unit>
    {
        private readonly IQuestionsRepository _questionsRepository;
        public DeleteQuestionCommandHandler(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }
        public async Task<Unit> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionsRepository.GetByIdAsync(request.Id);
            if (question == null)
                throw new NullReferenceException();
             await _questionsRepository.DeleteAsync(question);
            return new Unit();
        }
    }
}
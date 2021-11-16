using MediatR;
using Quiz.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Unit>
    {
        private readonly IQuestionsRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        public UpdateQuestionCommandHandler(IQuestionsRepository categoryRepository, IAnswerRepository answerRepository)
        {
            _questionRepository = categoryRepository;
            _answerRepository = answerRepository;
        }
        public async Task<Unit> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.Id);
            question.Content = request.QuestionContent;
            await _questionRepository.UpdateAsync(question);

            var answer = await _answerRepository.GetAnswerByQuestionId(request.Id);
            if(answer.Content != request.AnswerContent)
            {
                answer.Content = request.AnswerContent;
                await _answerRepository.UpdateAsync(answer);
            }
            return new Unit();
        }
    }
}
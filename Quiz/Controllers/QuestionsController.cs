using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Core.Application.Commands;
using Quiz.Core.Application.Queries;
using System;
using System.Threading.Tasks;

namespace Cns.ElementService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllQuestionsQuery());
            return Ok(result);
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var result = await _mediator.Send(new GetAllQuestionsByCategoryId() { CategoryId = categoryId });
            return Ok(result);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetQuestionWithAnswerById(int id)
        {
            var result = await _mediator.Send(new GetQuestionWithAnswerById() { QuestionId = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(UpdateQuestionModel updateQuestionModel, int id)
        {
            var command = new UpdateQuestionCommand()
            {
                QuestionContent = updateQuestionModel.QuestionContent,
                AnswerContent = updateQuestionModel.AnswerContent,
                Id = id
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var result = await _mediator.Send(new DeleteQuestionCommand() { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Route("answer/{questionId}")]
        public async Task<IActionResult> GetAnswerForQuestion(int questionId)
        {
            var result = await _mediator.Send(new GetAnswerByQuestionId() { QuestionId = questionId });
            return Ok(result);
        }
    }
}
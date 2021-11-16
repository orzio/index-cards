using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Core.Application.Commands;
using Quiz.Core.Application.Queries;
using Quiz.Core.Services;
using System;
using System.Threading.Tasks;

namespace Cns.ElementService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuizController(IMediator mediator)
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
        [Route("{id}/status")]
        public async Task<IActionResult> GetQuizStatusById(int id)
        {
            var result = await _mediator.Send(new GetQuizStatus() { QuizId = id });
            return Ok(result);
        }

        [HttpGet]
        [Route("question/{quizId}")]
        public async Task<IActionResult> GetQuestionByQuizId(int quizId)
        {
            var result = await _mediator.Send(new GetQuestionByQuizId() { QuizId = quizId });
            return Ok(result);
        }

        [HttpPost]
        [Route("category")]
        public async Task<IActionResult> Post(CreateNewQuizCommand CreateNewQuizCommand)
        {
            var result = await _mediator.Send(CreateNewQuizCommand);
            return Ok(result);
        }

        [HttpPost]
        [Route("checkanswer")]
        public async Task<IActionResult> Post(CheckAnswerCommand checkAnswerCommand)
        {
            var result = await _mediator.Send(checkAnswerCommand);
            return Ok(result);
        }
    }
}
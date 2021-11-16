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
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCategories());
            return Ok(result);
        }

        [HttpGet("quizes")]
        public async Task<IActionResult> GetAllCategoriesForQuiz()
        {
            var result = await _mediator.Send(new GetAllCategoriesForQuiz());
            return Ok(result);
        }

        [HttpGet("{id}/subcategories")]
        public async Task<IActionResult> GetSubcategories(int id)
        {
            var result = await _mediator.Send(new GetAllSubcategoriesQuery() { Id = id });
            return Ok(result);
        }

        [HttpGet("{id}/questions")]
        public async Task<IActionResult> GetQuestionForCategory(int id)
        {
            var result = await _mediator.Send(new GetAllQuestionsWithAnswerForCategory() { Id = id });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _mediator.Send(new GetCategoryById() { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostNewMainCategory(CreateMainCategoryCommand command)
        {
            //var command = new CreateMainCategoryCommand() { Name = name };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand updateCategoryCommand, int id)
        {
            var result = await _mediator.Send(updateCategoryCommand);
            return Ok(result);
        }

        [HttpPost("subcategories")]
        public async Task<IActionResult> PostSubcategory(CreateSubcategoryCommand createSubcategoryCommand)
        {
            var result = await _mediator.Send(createSubcategoryCommand);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand() { Id = id });
            return Ok(result);
        }
    }
}
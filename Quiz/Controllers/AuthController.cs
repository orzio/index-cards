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
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost("roles")]
        public async Task<IActionResult> PostRole(CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost("signup")]
        public async Task<IActionResult> PostNewUser(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost("addrole")]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
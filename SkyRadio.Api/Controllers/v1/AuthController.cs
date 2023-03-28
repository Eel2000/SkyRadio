using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkyRadio.Application.DTOs.Identity;
using SkyRadio.Application.Features.Identity.Commands;

namespace SkyRadio.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version}/Idenity/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUseAsyn([FromBody] RegistrationRequest request)
            => Ok(await _mediator.Send(new RegistrationCommand { RegistrationRequest = request }));

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticationUserAsync([FromBody] AuthenticationRequest request)
            => Ok(await _mediator.Send(new AuthenticationCommand { Authentication = request }));
    }
}

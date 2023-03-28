using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyRadio.Application.DTOs.Channels;
using SkyRadio.Application.Features.Channel.Commands;
using SkyRadio.Application.Features.Channel.Queries;
using SkyRadio.Domain.Entities;

namespace SkyRadio.Api.Controllers.v1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version}/Radio/[controller]")]
    public class ChannelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChannelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("user/{id}/channels-list")]
        [ProducesResponseType(typeof(IReadOnlyCollection<Channel>), StatusCodes.Status200OK)]
        public async ValueTask<IActionResult> GetListOfChannelByUser(string id)
            => Ok(await _mediator.Send(new UserChannelListQuery { UserId = id }));

        [AllowAnonymous]
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetChannelAsync(string id)
            => Ok(await _mediator.Send(new ChannelByIdQuery { ChannelId = id }));

        [HttpPost("create")]
        public async Task<IActionResult> CreateChannelAsync([FromBody] ChannelDto channel)
            => Ok(await _mediator.Send(new CreateChannelCommand { Channel = channel }));

        [HttpPut("edit")]
        public async Task<IActionResult> EditChannelAsync([FromBody] Channel channel)
            => Ok(await _mediator.Send(new EditChannelCommand { Channel = channel }));

        [AllowAnonymous]
        [HttpGet("available-channels")]
        public async Task<IActionResult> GetChannelsAvailableAsync()
            => Ok(await _mediator.Send(new ChannelsListQuery()));
    }
}

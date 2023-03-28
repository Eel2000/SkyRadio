using MediatR;
using SkyRadio.Application.DTOs.Channels;
using SkyRadio.Application.Interfaces.Services;
using SkyRadio.Domain.Commons;

namespace SkyRadio.Application.Features.Channel.Commands
{
    public class CreateChannelCommand : IRequest<Response<object>>
    {
        public ChannelDto Channel { get; set; }
    }

    public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, Response<object>>
    {
        private readonly IChannelService _channelService;

        public CreateChannelCommandHandler(IChannelService channelService)
        {
            _channelService = channelService;
        }

        public async Task<Response<object>> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
        {
            return await _channelService.CreateAsync(request.Channel);
        }
    }
}

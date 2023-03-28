using MediatR;
using SkyRadio.Application.Interfaces.Services;
using SkyRadio.Domain.Commons;

namespace SkyRadio.Application.Features.Channel.Queries;

public class ChannelByIdQuery : IRequest<Response<Domain.Entities.Channel>>
{
    public string ChannelId { get; set; }
}

public class ChannelByIdQueryHandler : IRequestHandler<ChannelByIdQuery, Response<Domain.Entities.Channel>>
{
    private readonly IChannelService _channelService;

    public ChannelByIdQueryHandler(IChannelService channelService)
    {
        _channelService = channelService;
    }

    public async Task<Response<Domain.Entities.Channel>> Handle(ChannelByIdQuery request, CancellationToken cancellationToken)
    {
        return await _channelService.GetChannelAsync(request.ChannelId);
    }
}

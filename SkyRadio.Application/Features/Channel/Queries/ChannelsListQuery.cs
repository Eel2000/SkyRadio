using MediatR;
using SkyRadio.Application.Interfaces.Services;
using SkyRadio.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyRadio.Application.Features.Channel.Queries
{
    public class ChannelsListQuery : IRequest<Response<IReadOnlyCollection<Domain.Entities.Channel>>>
    {
    }

    public class ChannelsListQueryHandler : IRequestHandler<ChannelsListQuery, Response<IReadOnlyCollection<Domain.Entities.Channel>>>
    {
        private readonly IChannelService _channelService;

        public ChannelsListQueryHandler(IChannelService channelService)
        {
            _channelService = channelService;
        }

        public async Task<Response<IReadOnlyCollection<Domain.Entities.Channel>>> Handle(ChannelsListQuery request, CancellationToken cancellationToken)
        {
            return await _channelService.GetChannelsAsync();
        }
    }
}

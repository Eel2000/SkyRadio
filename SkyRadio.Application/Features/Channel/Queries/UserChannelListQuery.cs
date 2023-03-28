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
    public class UserChannelListQuery : IRequest<Response<IReadOnlyCollection<Domain.Entities.Channel>>>
    {
        public string UserId { get; set; }
    }

    public class UserChannelListQueryHandler : IRequestHandler<UserChannelListQuery, Response<IReadOnlyCollection<Domain.Entities.Channel>>>
    {
        private readonly IChannelService _channelService;

        public UserChannelListQueryHandler(IChannelService channelService)
        {
            _channelService = channelService;
        }

        public async Task<Response<IReadOnlyCollection<Domain.Entities.Channel>>> Handle(UserChannelListQuery request, CancellationToken cancellationToken)
        {
            return await _channelService.GetChannelsAsync(request.UserId);
        }
    }
}

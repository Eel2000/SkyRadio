using MediatR;
using SkyRadio.Application.Interfaces.Services;
using SkyRadio.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyRadio.Application.Features.Channel.Commands
{
    public class EditChannelCommand : IRequest<Response<object>>
    {
        public Domain.Entities.Channel Channel { get; set; }
    }

    public class EditChannelCommandHandler : IRequestHandler<EditChannelCommand, Response<object>>
    {
        private readonly IChannelService _channelService;

        public EditChannelCommandHandler(IChannelService channelService)
        {
            _channelService = channelService;
        }

        public async Task<Response<object>> Handle(EditChannelCommand request, CancellationToken cancellationToken)
        {
            return await _channelService.EditAsync(request.Channel);
        }
    }
}

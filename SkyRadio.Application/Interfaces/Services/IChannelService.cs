using SkyRadio.Application.DTOs.Channels;
using SkyRadio.Domain.Commons;
using SkyRadio.Domain.Entities;

namespace SkyRadio.Application.Interfaces.Services;

public interface IChannelService
{
    ValueTask<Response<Channel>> GetChannelAsync(string channelId);
    ValueTask<Response<IReadOnlyCollection<Channel>>> GetChannelsAsync();
    ValueTask<Response<IReadOnlyCollection<Channel>>> GetChannelsAsync(string userdId);
    ValueTask<Response<object>> CreateAsync(ChannelDto channel);
    ValueTask<Response<object>> EditAsync(Channel channel);
}

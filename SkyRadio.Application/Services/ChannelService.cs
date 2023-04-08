using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkyRadio.Application.DTOs.Channels;
using SkyRadio.Application.Interfaces.Services;
using SkyRadio.Application.Mappings;
using SkyRadio.Domain.Commons;
using SkyRadio.Domain.Entities;
using SkyRadio.Persistence.Contexts;

namespace SkyRadio.Application.Services;

public class ChannelService : IChannelService
{
    private readonly SkyRadioDbContext _context;
    private readonly ILogger<ChannelService> _logger;

    public ChannelService(SkyRadioDbContext context, ILogger<ChannelService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async ValueTask<Response<object>> CreateAsync(ChannelDto channel)
    {

        if (await _context.Channels.AnyAsync(x => x.Name.ToLower() == channel.Name.ToLower()))
        {
            _logger.LogError("Channel name already taken");
            throw new OperationCanceledException("This channel name is already taken");
        }

        var mapper = new ChannelMapper();
        var chnl = mapper.Map(channel);

        var ownerChannel = new ChannelOwner
        {
            ChannelId = chnl.Id,
            OwnerId = channel.OwnerId
        };

        await _context.Channels.AddAsync(chnl);
        await _context.ChannelOwner.AddAsync(ownerChannel);

        await _context.SaveChangesAsync();

        _logger.LogInformation("new channel created");
        return new Response<object>(isSucceed: true, "Channel created");
    }

    public async ValueTask<Response<object>> EditAsync(Channel channel)
    {
        _context.Channels.Update(channel);
        await _context.SaveChangesAsync();

        return new Response<object>(isSucceed: true, "edition completed");
    }

    public async ValueTask<Response<Channel>> GetChannelAsync(string channelId)
    {
        var data = await _context.Channels.FirstOrDefaultAsync(x => x.Id == channelId);
        return new Response<Channel>(data, "Data found");
    }

    public async ValueTask<Response<IReadOnlyCollection<Channel>>> GetChannelsAsync()
    {
        var data = await _context.Channels.ToListAsync();
        return new Response<IReadOnlyCollection<Channel>>(data, "data found");
    }

    public async ValueTask<Response<IReadOnlyCollection<Channel>>> GetChannelsAsync(string userdId)
    {
        var data = await _context.ChannelOwner.Where(x => x.OwnerId == userdId).ToListAsync();

        var channels = new List<Channel>();
        foreach (var channelOwner in data)
        {
            var channel = await _context.Channels.FirstOrDefaultAsync(x => x.Id == channelOwner.ChannelId);
            if (channel != null) channels.Add(channel);
        }

        return new Response<IReadOnlyCollection<Channel>>(channels, "user {userId} channels");
    }
}

using Microsoft.EntityFrameworkCore;
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

    public ChannelService(SkyRadioDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Response<object>> CreateAsync(ChannelDto channel)
    {
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

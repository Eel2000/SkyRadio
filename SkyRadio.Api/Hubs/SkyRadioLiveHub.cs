using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using SkyRadio.Domain.Entities;

namespace SkyRadio.Api.Hubs
{
    public class SkyRadioLiveHub : Hub<ISkyRadioLiveHub>
    {

        [Authorize]
        public async Task BroadCast(Playload packet)
        {
            await Clients.Group(packet.ChannelId).Listening(packet);
        }

        /// <summary>
        /// notify every connected user that a specifique channel has started a session.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns><see cref="Task"/></returns>
        [Authorize]
        public async Task StartBroadcast(Channel channel)
        {
            await Clients.All.BroadCastStarted(channel);
            Log.Information($"Channel {channel.Name} has start a broadcast");
        }

        /// <summary>
        /// Notify everyone that the channel has started broadcasting.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns><see cref="Task"/></returns>
        [Authorize]
        public async Task StopBroadcast(Channel channel)
        {
            await Clients.All.BroadCastStopped(channel);
            Log.Information($"Channel {channel.Name} stoped to broadcast ");
        }

        /// <summary>
        /// Join the specific channel room to listen to.
        /// </summary>
        /// <param name="channel">Channel uuid</param>
        /// <returns><see cref="Task"/></returns>
        public Task EnterChannel(string channel)
            => Groups.AddToGroupAsync(connectionId: Context.ConnectionId, channel);

        /// <summary>
        /// Leave(stop) specific channle
        /// </summary>
        /// <param name="channel">channel uuid</param>
        /// <returns><see cref="Task"/></returns>
        public Task LeaveChannel(string channel)
            => Groups.RemoveFromGroupAsync(Context.ConnectionId, channel);
    }
}

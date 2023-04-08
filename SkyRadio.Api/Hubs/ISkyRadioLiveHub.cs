using SkyRadio.Domain.Entities;

namespace SkyRadio.Api.Hubs
{
    public interface ISkyRadioLiveHub
    {
        Task Listening(Playload packet);
        Task CountListeners(string channel);
        Task BroadCastStarted(Channel channel);
        Task BroadCastStopped(Channel channel);
    }
}

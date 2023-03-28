using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SkyRadio.Api.Hubs
{
    //[Authorize]
    public class SkyRadioLiveHub : Hub<ISkyRadioLiveHub>
    {
    }
}

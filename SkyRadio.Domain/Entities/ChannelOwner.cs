using System.ComponentModel.DataAnnotations;

namespace SkyRadio.Domain.Entities;

/// <summary>
/// Binding table between owner(user/host) and channels(their). 
/// </summary>
public class ChannelOwner
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string ChannelId { get; set; }
    public required string OwnerId { get; set; }
}

using SkyRadio.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SkyRadio.Domain.Entities;

/// <summary>
/// Represent the radio channel to listen to. 
/// Its id will be use add group name when trying to listening to.
/// </summary>
public class Channel
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required ChannelType Type { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
}

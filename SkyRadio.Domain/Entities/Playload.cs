namespace SkyRadio.Domain.Entities;

public class Playload
{
    public required string ChannelId { get; set; }
    public required byte[] Buffer { get; set; }
    public required int BuffuredDataLength { get; set; }

}

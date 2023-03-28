using Riok.Mapperly.Abstractions;
using SkyRadio.Application.DTOs.Channels;
using SkyRadio.Domain.Entities;

namespace SkyRadio.Application.Mappings
{
    [Mapper]
    public partial class ChannelMapper
    {
        public partial ChannelDto Map(Channel channel);

        public partial IReadOnlyCollection<ChannelDto> MapList(List<Channel> channel);

        public partial Channel Map(ChannelDto channel);
    }
}

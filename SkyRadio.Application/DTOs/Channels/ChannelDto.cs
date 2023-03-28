using SkyRadio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyRadio.Application.DTOs.Channels
{
    public class ChannelDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required ChannelType Type { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage ="Please specify the owner")]
        public string OwnerId { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}

using Microsoft.EntityFrameworkCore;
using SkyRadio.Domain.Entities;

namespace SkyRadio.Persistence.Contexts;

public class SkyRadioDbContext : DbContext
{
    public SkyRadioDbContext()
    {
        
    }

    public SkyRadioDbContext(DbContextOptions<SkyRadioDbContext> options):base(options)
    {
        
    }

    public virtual DbSet<Channel> Channels { get; set; }
    public virtual DbSet<ChannelOwner> ChannelOwner { get; set; }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SkyRadio.Persistence.Contexts;

public class SkyRadioIdentityDbContext : IdentityDbContext
{
    public SkyRadioIdentityDbContext(DbContextOptions<SkyRadioIdentityDbContext> options) : base(options)
    {

    }
}

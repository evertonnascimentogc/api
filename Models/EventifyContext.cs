using Microsoft.EntityFrameworkCore;

namespace EventifyApi.Models;

public class EventifyContext : DbContext
{
    public EventifyContext(DbContextOptions<EventifyContext> options)
        : base(options)
    {

    }

    public DbSet<Eventify.Models.EventifyItem> EventifyItems { get; set; } = null!;
}
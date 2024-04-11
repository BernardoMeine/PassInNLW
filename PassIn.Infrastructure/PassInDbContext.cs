using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure;
public class PassInDbContext : DbContext
{
    public PassInDbContext(DbContextOptions options) 
        : base(options)
    {

    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<CheckIn> CheckIns { get; set; }
}

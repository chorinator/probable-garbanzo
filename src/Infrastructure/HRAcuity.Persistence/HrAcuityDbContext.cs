using HRAcuity.Persistence.Entities;

namespace HRAcuity.Persistence;

public class HrAcuityDbContext(DbContextOptions<HrAcuityDbContext> options)
    : DbContext(options)
{
    public DbSet<NotableQuoteEntity> NotableQuotes { get; set; }
}
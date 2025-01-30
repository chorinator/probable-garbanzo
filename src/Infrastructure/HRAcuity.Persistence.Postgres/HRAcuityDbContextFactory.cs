using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HRAcuity.Persistence.Postgres;

public class HRAcuityDbContextFactory : IDesignTimeDbContextFactory<HrAcuityDbContext>
{
    public HrAcuityDbContext CreateDbContext(string[] args)
    {
        var connectionString = GetConnectionString(args);

        var optionsBuilder = new DbContextOptionsBuilder<HrAcuityDbContext>();
        optionsBuilder.UseNpgsql(connectionString,
            o => 
                o.MigrationsAssembly(typeof(HRAcuityDbContextFactory).Assembly.FullName));

        return new HrAcuityDbContext(optionsBuilder.Options);
    }
    
    private static string GetConnectionString(string[] args)
    {
        return args.Length > 0 
            ? args[0] 
            : File.ReadAllText("connectionstring.txt");
    }
}
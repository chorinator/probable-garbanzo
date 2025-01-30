using HRAcuity.Persistence;
using HRAcuity.Persistence.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration.Mocks;

public class HrAcuityDbContextFactoryMock(
    HRAcuityDbContextFactory dbContextFactory,
    string connectionString)
    : IDbContextFactory<HrAcuityDbContext>
{
    public HrAcuityDbContext CreateDbContext() =>
        dbContextFactory.CreateDbContext([connectionString]);
}
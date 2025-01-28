using PQAMCAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class PQAMCAPIContext : DbContext
{
    public PQAMCAPIContext(DbContextOptions<PQAMCAPIContext> options)
        : base(options)
    {
    }

    public DbSet<User> User { get; set; } = null;
    public DbSet<UserClaim> UserClaim { get; set; } = null;
    public DbSet<UserToken> UserToken { get; set; } = null;
    public DbSet<UserRefreshToken> UserRefreshToken { get; set; } = null;
    public DbSet<Country> Country { get; set; } = null;
    public DbSet<City> City { get; set; } = null;
    public DbSet<Area> Area { get; set; } = null;
    public DbSet<Bank> Bank { get; set; } = null;
    public DbSet<ContactOwnerShip> ContactOwnerShip { get; set; } = null;
    public DbSet<Education> Education { get; set; } = null;
    public DbSet<Gender> Gender { get; set; } = null;
    public DbSet<Occupation> Occupation{ get; set; } = null;
    public DbSet<Profession> Profession { get; set; } = null;
    public DbSet<AccountCategory> AccountCategory { get; set; } = null;
    public DbSet<IncomeSource> SourceIncome{ get; set; } = null;
    public DbSet<AnnualIncome> AnnualIncome { get; set; } = null;
    public DbSet<ResidentialStatus> ResidentialStatus { get; set; } = null;
    public DbSet<ITMindsRequest> ITMindsRequests { get; set; } = null;
    public DbSet<VPSPlan> VPSPlan { get; set; } = null;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Model.SetMaxIdentifierLength(30);

    }

}
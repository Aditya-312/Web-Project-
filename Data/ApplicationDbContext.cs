using CampusCare.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusCare.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Complaint> Complaints => Set<Complaint>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Complaint>().HasIndex(c => c.CreatedAt);
    }
}

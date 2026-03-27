using Microsoft.EntityFrameworkCore;
using TripPlanner.DTO;

namespace TripPlanner.Model
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<item_types> item_types { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseSummaryDto>().HasNoKey();
        }
    }
}

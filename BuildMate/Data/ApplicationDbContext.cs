using Microsoft.EntityFrameworkCore;
using Buildmate.Models;

namespace Buildmate.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RfqRequest> RfqRequests => Set<RfqRequest>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Quotation> Quotations => Set<Quotation>();
        public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<CompanyProfile> CompanyProfiles => Set<CompanyProfile>();
        public DbSet<CompanyDocument> CompanyDocuments => Set<CompanyDocument>();
        public DbSet<NotificationItem> Notifications => Set<NotificationItem>();
        public DbSet<UserSettings> UserSettings => Set<UserSettings>();
        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
        public DbSet<MessageThread> MessageThreads => Set<MessageThread>();
        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
           
            modelBuilder.Entity<CompanyProfile>()
            .Property(c => c.AverageRating)
            .HasColumnType("decimal(3,2)");
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WalletAppBackend.Entities;
using WalletAppBackend.Enums;

namespace WalletAppBackend.Persistence
{
    public class MainDbContext : DbContext, IMainDbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<IEntity>().AsEnumerable())
            {
                //Auto Timestamp
                item.Entity.CreatedAt = DateTime.UtcNow;
                item.Entity.UpdatedAt = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Transactions)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Transaction>()
             .HasOne(e => e.AuthorizedUser)
             .WithMany(e => e.AuthorizedTransactions)
             .OnDelete(DeleteBehavior.SetNull);


            //Seed some data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "Admin", CardBalance = 100 }
                );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, Status = TransactionStatus.Processed, Type = TransactionType.Payment, Name = "Apple", Sum = 12.5M, UserId = 1, Description = "AppStore Payment" }
                );
        }
    }
}

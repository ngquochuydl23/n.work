using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using n.work.Entity;
using n.work.Models;

namespace n.work.DataContext
{
  public class DatabaseContext : DbContext
  {
    public DbSet<Account> Account { get; set; }
    public DbSet<Profile> Profile { get; set; }
    public DbSet<WorkerAccount> WorkerAccount { get; set; }
    public DbSet<WorkerProfile> WorkerProfile { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<ItemOrder> ItemOrder { get; set; }
    public DbSet<OrderDetail> OrderDetail { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Map table names
      CreateAccountModelMap(modelBuilder.Entity<Account>());
      CreateProfileModelMap(modelBuilder.Entity<Profile>());
      //Image
      CreateImageModelMap(modelBuilder.Entity<Image>());
      //Partner
      CreateWorkderAccountModelMap(modelBuilder.Entity<WorkerAccount>());
      CreateWorkerProfileModelMap(modelBuilder.Entity<WorkerProfile>());
      //Activity
      CreateActivityModelMap(modelBuilder.Entity<OrderDetail>());
      CreateItemActivityModelMap(modelBuilder.Entity<ItemOrder>());

      base.OnModelCreating(modelBuilder);
    }

    private void CreateImageModelMap(EntityTypeBuilder<Image> builder)
    {
      builder.ToTable(nameof(Images));
      builder.HasKey(itemActivity => itemActivity.ImageId);
    }

    private void CreateActivityModelMap(EntityTypeBuilder<OrderDetail> builder)
    {
      builder.ToTable(nameof(OrderDetail));
      builder.HasKey(order => order.OrderId);
      builder
        .HasOne(activity => activity.ItemOrder)
        .WithOne(itemActivity => itemActivity.ActivityDetail)
        .HasForeignKey<ItemOrder>(itemActivity => itemActivity.ActivityId)
        .IsRequired();

      builder
        .HasOne(order => order.Worker)
        .WithOne(worker => worker.OrderDetail)
        .HasForeignKey<OrderDetail>(order => order.OrderId)
        .IsRequired();
    }

    private void CreateItemActivityModelMap(EntityTypeBuilder<ItemOrder> builder)
    {
      builder.HasKey(itemActivity => itemActivity.ActivityId);
    }

    private void CreateAccountModelMap(EntityTypeBuilder<Account> builder)
    {
      builder.ToTable(nameof(Account));
      builder.HasKey(account => account.Id);
      builder
        .HasOne(acc => acc.Profile)
        .WithOne(profile => profile.Account)
        .HasForeignKey<Profile>(profile => profile.AccountId)
        .IsRequired();
    }

    private void CreateProfileModelMap(EntityTypeBuilder<Profile> builder)
    {
      builder.HasKey(profile => profile.AccountId);
    }

    private void CreateWorkderAccountModelMap(EntityTypeBuilder<WorkerAccount> builder)
    {
      builder.ToTable(nameof(WorkerAccount));
      builder.HasKey(account => account.Id);

      builder
        .HasOne(acc => acc.WorkerProfile)
        .WithOne(profile => profile.WorkerAccount)
        .HasForeignKey<WorkerProfile>(profile => profile.AccountId)
        .IsRequired();

    }

    private void CreateWorkerProfileModelMap(EntityTypeBuilder<WorkerProfile> builder)
    {
      builder.HasKey(profile => profile.AccountId);
    }
  }
}

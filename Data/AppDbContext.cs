using Ampifan.Models;
using Microsoft.EntityFrameworkCore;

public class AppDBContext : DbContext
{
    public DbSet<ActiveChat> ActiveChats { get; set; }
    public DbSet<Analytic> Analytics { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<InviteCode> InviteCodes { get; set; }
    public DbSet<Talent> Talents { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserConnection> UserConnections { get; set; }
    public DbSet<Video> Videos { get; set; }

    public string DbPath { get; }
    public AppDBContext(DbContextOptions<AppDBContext> options)
                : base(options)
    {
    }
    // public AppDBContext()
    // {
    //     var folder = Environment.SpecialFolder.LocalApplicationData;
    //     var path = Environment.GetFolderPath(folder);
    //     DbPath = System.IO.Path.Join(path, "ampifan.db");
    // }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatMessage>()
       .HasOne(p => p.User)
       .WithMany(p => p.ChatMessages)
       .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<User>()
       .HasOne(p => p.Connection)
       .WithOne(p => p.User)
       .HasForeignKey<Connection>(p => p.UserId);

        modelBuilder.Entity<User>()
       .HasOne(p => p.InviteCode)
       .WithOne(p => p.User)
       .HasForeignKey<InviteCode>(p => p.UserId);

        modelBuilder.Entity<User>()
       .HasOne(p => p.Talent)
       .WithOne(p => p.User)
       .HasForeignKey<Talent>(p => p.UserId)
       .OnDelete(DeleteBehavior.Cascade);

       modelBuilder.Entity<Talent>()
       .HasOne(u => u.User)
       .WithOne(p => p.Talent)
       .HasForeignKey<Talent>(p => p.UserId)
       .OnDelete(DeleteBehavior.Cascade);

       modelBuilder.Entity<Talent>()
       .HasOne(t => t.Category)
       .WithMany(t => t.Talents)
       .HasForeignKey(t => t.CategoryId);

        modelBuilder.Entity<User>()
       .HasMany(p => p.ChatMessages)
       .WithOne(p => p.User)
       .HasForeignKey(p => p.UserId);


    }
}
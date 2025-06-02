using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Zoo.REST.Models;

namespace Zoo.REST
{
    public class ZooContextFactory : IDesignTimeDbContextFactory<ZooContext>
    {
        public ZooContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ZooContext>();
            optionsBuilder.UseSqlite("Data Source=zoo.db");

            return new ZooContext(optionsBuilder.Options);
        }
    }

    public class ZooContext : DbContext
    {
        public DbSet<ZooModel> Zoos { get; set; }
        public DbSet<AnimalModel> Animals { get; set; }
        public DbSet<BirdModel> Birds { get; set; }
        public DbSet<ZookeeperModel> Zookeepers { get; set; }
        public DbSet<AnimalZookeeperModel> AnimalZookeepers { get; set; }

        public ZooContext(DbContextOptions<ZooContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimalModel>().ToTable("Animals");
            modelBuilder.Entity<BirdModel>().ToTable("Birds");

            // Zookeeper -> Animal
            modelBuilder.Entity<ZookeeperModel>()
                .HasOne(z => z.Animal)
                .WithOne(a => a.Zookeeper)
                .HasForeignKey<AnimalModel>(a => a.ZookeeperId)
                .OnDelete(DeleteBehavior.SetNull);

            // Zoo -> Animals
            modelBuilder.Entity<ZooModel>()
                .HasMany(z => z.Animals)
                .WithOne(a => a.Zoo)
                .HasForeignKey(a => a.ZooId);

            // Zoo -> Zookeepers
            modelBuilder.Entity<ZooModel>()
                .HasMany(z => z.Zookeepers)
                .WithOne(zk => zk.Zoo)
                .HasForeignKey(zk => zk.ZooId);

            //  Animal <-> Zookeeper через проміжну таблицю
            modelBuilder.Entity<AnimalZookeeperModel>()
                .HasKey(az => new { az.AnimalId, az.ZookeeperId });

            modelBuilder.Entity<AnimalZookeeperModel>()
                .HasOne(az => az.Animal)
                .WithMany()
                .HasForeignKey(az => az.AnimalId);

            modelBuilder.Entity<AnimalZookeeperModel>()
                .HasOne(az => az.Zookeeper)
                .WithMany()
                .HasForeignKey(az => az.ZookeeperId);
        }
    }
}

using WardrobeAPI.Entities;

namespace WardrobeAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){ }

        public DbSet<Apparel> Apparel { get; set; }

        public DbSet<Closet> Closet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Closet>().HasData(
                new Closet
                {
                    Id = 1,
                    Name = "det grå skab"
                },
                new Closet
                {
                    Id = 2,
                    Name = "Det grønne skab"
                }
                ); 

            modelBuilder.Entity<Apparel>().HasData(
                new Apparel
                {
                    Id = 1,
                    Title = "Den blå Skjorte",
                    Description = "Den blå skjorte med hvide prikker",
                    Color = "Blå",
                    ClosetId= 1
                },
                new Apparel
                {
                    Id = 2,
                    Title = "Sorte Jeans",
                    Description = "Det storte wrangler",
                    Color = "Sort",
                    ClosetId= 2
                }
                );
        }
    }
}

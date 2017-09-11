using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Models;
using SchoolLib.Models.People;
using SchoolLib.Models.Books;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SchoolLib.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Reader>         Readers         { get; set; }
        public DbSet<Student>        Students        { get; set; }
        public DbSet<Worker>         Workers         { get; set; }
        public DbSet<Drop>           Drops           { get; set; }

        public DbSet<Book>           Books           { get; set; }
        public DbSet<AdditionalBook> AdditionalBooks { get; set; }
        public DbSet<StudyBook>      StudyBooks       { get; set; }
        public DbSet<ExternalBook>   ExternalBooks   { get; set; }
        public DbSet<Inventory>      Inventories     { get; set; }
        public DbSet<Issuance>       Issuances       { get; set; }
        public DbSet<Provenance>     Provenances     { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Provenance>().HasIndex(p => p.WayBill).IsUnique();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolLib;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}

//builder.Entity<Reader>()
//       .HasOne(p => p.ReaderProfile)
//       .WithOne(t => t.Reader)
//       .OnDelete(DeleteBehavior.Restrict);
//builder.Entity<User>().Property(u => u.Age).HasDefaultValue(18);
//builder.Entity<AdditionalBook>().Property(b => b.Cipher).IsRequired();
//builder.Entity<AdditionalBook>().Property(b => b.Language).IsRequired();

// Customize the ASP.NET Identity model and override the defaults if needed.
// For example, you can rename the ASP.NET Identity table names and more.
// Add your customizations after calling base.OnModelCreating(builder);
//Index(IsUnique= true)
//Team team = db.Teams
//    .Include(t => t.Players)
//        .ThenInclude(p => p.Country)
//            .ThenInclude(c => c.Capital)
//    .Include(t => t.Stadium)
//    .FirstOrDefault();

// db.Players.Where(p=>p.TeamId==team.Id).Load();
//Team team = db.Teams.FirstOrDefault();
//db.Entry(team).Collection(t=>t.Players).Load();

//Player player = db.Players.FirstOrDefault();
//db.Entry(player).Reference(x => x.Team).Load();
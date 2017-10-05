using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Models;
using SchoolLib.Models.People;
using SchoolLib.Models.Books;

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
        public DbSet<StudyBook>      StudyBooks      { get; set; }
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
    }
}
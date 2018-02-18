using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolLib.Models;
using SchoolLib.Models.Books;
using SchoolLib.Models.People;

namespace SchoolLib.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Reader> Readers { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Worker> Workers { get; set; }
		public DbSet<Drop> Drops { get; set; }

		public DbSet<Book> Books { get; set; }
		public DbSet<AdditionalBook> AdditionalBooks { get; set; }
		public DbSet<StudyBook> StudyBooks { get; set; }
		public DbSet<ExternalBook> ExternalBooks { get; set; }
		public DbSet<Inventory> Inventories { get; set; }
		public DbSet<Issuance> Issuances { get; set; }
		public DbSet<Provenance> Provenances { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Provenance>().HasIndex(p => p.WayBill).IsUnique();
		}
	}
}
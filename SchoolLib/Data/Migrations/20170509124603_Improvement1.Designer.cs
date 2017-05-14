using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SchoolLib.Data;
using SchoolLib.Models.Books;
using SchoolLib.Models.People;

namespace SchoolLib.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170509124603_Improvement1")]
    partial class Improvement1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SchoolLib.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SchoolLib.Models.Books.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("AuthorCipher")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("Published")
                        .HasColumnType("date");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Book");
                });

            modelBuilder.Entity("SchoolLib.Models.Books.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActNumber");

                    b.Property<int>("BookId");

                    b.Property<string>("Couse")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Note")
                        .HasMaxLength(250);

                    b.Property<DateTime>("Year");

                    b.HasKey("Id");

                    b.HasIndex("BookId")
                        .IsUnique();

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("SchoolLib.Models.Books.Issuance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AcceptanceDate")
                        .HasColumnType("date");

                    b.Property<int>("BookId");

                    b.Property<string>("Couse")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("date");

                    b.Property<string>("Note")
                        .HasMaxLength(250);

                    b.Property<int>("ReaderId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReaderId");

                    b.ToTable("Issuances");
                });

            modelBuilder.Entity("SchoolLib.Models.Books.Provenance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<string>("Note")
                        .HasMaxLength(250);

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("ReceiptDate");

                    b.Property<int>("WayBill");

                    b.HasKey("Id");

                    b.HasIndex("BookId")
                        .IsUnique();

                    b.ToTable("Provenances");
                });

            modelBuilder.Entity("SchoolLib.Models.People.Drop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Couse")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Note")
                        .HasMaxLength(250);

                    b.Property<int>("ReaderId");

                    b.HasKey("Id");

                    b.HasIndex("ReaderId");

                    b.ToTable("Drops");
                });

            modelBuilder.Entity("SchoolLib.Models.People.Reader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ReaderId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("FirstRegistrationDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("LastRegistrationDate")
                        .HasColumnType("date");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Readers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Reader");
                });

            modelBuilder.Entity("SchoolLib.Models.People.ReaderProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Apartment");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<short>("House");

                    b.Property<string>("Patronimic")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<int>("ReaderId");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.HasIndex("ReaderId")
                        .IsUnique();

                    b.ToTable("ReaderProfiles");
                });

            modelBuilder.Entity("SchoolLib.Models.Books.AdditionalBook", b =>
                {
                    b.HasBaseType("SchoolLib.Models.Books.Book");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.ToTable("AdditionalBook");

                    b.HasDiscriminator().HasValue("AdditionalBook");
                });

            modelBuilder.Entity("SchoolLib.Models.Books.ExternalBook", b =>
                {
                    b.HasBaseType("SchoolLib.Models.Books.Book");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.ToTable("ExternalBook");

                    b.HasDiscriminator().HasValue("ExternalBook");
                });

            modelBuilder.Entity("SchoolLib.Models.Books.StudyBook", b =>
                {
                    b.HasBaseType("SchoolLib.Models.Books.Book");

                    b.Property<int>("Grade");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.ToTable("StudyBook");

                    b.HasDiscriminator().HasValue("StudyBook");
                });

            modelBuilder.Entity("SchoolLib.Models.People.Student", b =>
                {
                    b.HasBaseType("SchoolLib.Models.People.Reader");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.ToTable("Readers");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("SchoolLib.Models.People.Worker", b =>
                {
                    b.HasBaseType("SchoolLib.Models.People.Reader");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.ToTable("Readers");

                    b.HasDiscriminator().HasValue("Worker");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SchoolLib.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SchoolLib.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolLib.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolLib.Models.Books.Inventory", b =>
                {
                    b.HasOne("SchoolLib.Models.Books.Book", "Book")
                        .WithOne("Inventory")
                        .HasForeignKey("SchoolLib.Models.Books.Inventory", "BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolLib.Models.Books.Issuance", b =>
                {
                    b.HasOne("SchoolLib.Models.Books.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolLib.Models.People.Reader", "Reader")
                        .WithMany()
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolLib.Models.Books.Provenance", b =>
                {
                    b.HasOne("SchoolLib.Models.Books.Book", "Book")
                        .WithOne("Provenance")
                        .HasForeignKey("SchoolLib.Models.Books.Provenance", "BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolLib.Models.People.Drop", b =>
                {
                    b.HasOne("SchoolLib.Models.People.Reader", "Reader")
                        .WithMany()
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolLib.Models.People.ReaderProfile", b =>
                {
                    b.HasOne("SchoolLib.Models.People.Reader", "Reader")
                        .WithOne("ReaderProfile")
                        .HasForeignKey("SchoolLib.Models.People.ReaderProfile", "ReaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

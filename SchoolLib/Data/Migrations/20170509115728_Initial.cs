using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SchoolLib.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(maxLength: 25, nullable: false),
                    AuthorCipher = table.Column<string>(maxLength: 15, nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Note = table.Column<string>(maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Published = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Genre = table.Column<string>(maxLength: 20, nullable: true),
                    Language = table.Column<string>(maxLength: 15, nullable: true),
                    Grade = table.Column<int>(nullable: true),
                    Subject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    ReaderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    FirstRegistrationDate = table.Column<DateTime>(nullable: false),
                    LastRegistrationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Grade = table.Column<string>(maxLength: 10, nullable: true),
                    Position = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.ReaderId);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActNumber = table.Column<int>(nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    Couse = table.Column<string>(maxLength: 30, nullable: false),
                    Note = table.Column<string>(maxLength: 250, nullable: true),
                    Year = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provenances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(maxLength: 250, nullable: true),
                    Place = table.Column<string>(maxLength: 30, nullable: false),
                    ReceiptDate = table.Column<DateTime>(nullable: false),
                    WayBill = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provenances_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issuances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcceptanceDate = table.Column<DateTime>(nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    Couse = table.Column<string>(maxLength: 30, nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(maxLength: 250, nullable: true),
                    ReaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issuances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issuances_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issuances_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "ReaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Couse = table.Column<string>(maxLength: 30, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(maxLength: 250, nullable: true),
                    ReaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drops_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "ReaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReaderProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Apartment = table.Column<short>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 10, nullable: false),
                    House = table.Column<short>(nullable: false),
                    Patronimic = table.Column<string>(maxLength: 15, nullable: false),
                    ReaderId = table.Column<int>(nullable: false),
                    Street = table.Column<string>(maxLength: 15, nullable: false),
                    SurName = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReaderProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReaderProfiles_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "ReaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_BookId",
                table: "Inventories",
                column: "BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issuances_BookId",
                table: "Issuances",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Issuances_ReaderId",
                table: "Issuances",
                column: "ReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Provenances_BookId",
                table: "Provenances",
                column: "BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drops_ReaderId",
                table: "Drops",
                column: "ReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReaderProfiles_ReaderId",
                table: "ReaderProfiles",
                column: "ReaderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Issuances");

            migrationBuilder.DropTable(
                name: "Provenances");

            migrationBuilder.DropTable(
                name: "Drops");

            migrationBuilder.DropTable(
                name: "ReaderProfiles");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}

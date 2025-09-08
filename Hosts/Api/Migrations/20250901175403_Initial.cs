using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Olympus.Api.Host.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Username = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Password = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    JobTitle = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Department = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    OfficeLocation = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Country = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Photo = table.Column<byte[]>(type: "bytea", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Locked = table.Column<bool>(type: "boolean", nullable: false),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Locked = table.Column<bool>(type: "boolean", nullable: false),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examples_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Examples_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "Country", "CreatedAt", "CreatedById", "Department", "Email", "Hidden", "JobTitle", "Locked", "Name", "OfficeLocation", "Password", "Photo", "UpdatedAt", "UpdatedById", "Username", "Version" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000001"), null, "unknown@user.com", true, null, true, "Unknown User", null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000001"), null, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000002"), null, "external@user.com", true, null, true, "External User", null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000002"), null, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000003"), null, "service@user.com", true, null, true, "Service User", null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000003"), null, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000004"), null, "agent@user.com", true, null, true, "Agent User", null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000004"), null, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000005"), null, "system@user.com", true, null, true, "System User", null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000005"), null, new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examples_CreatedById",
                table: "Examples",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_UpdatedById",
                table: "Examples",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_Version",
                table: "Examples",
                column: "Version");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedById",
                table: "Users",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedById",
                table: "Users",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Version",
                table: "Users",
                column: "Version");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examples");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

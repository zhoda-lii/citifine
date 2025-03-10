using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CitiFine.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "75dab2d3-add7-448c-b410-632f0c182751", null, "User", "USER" },
                    { "93ff95e8-0f2e-422a-a966-240284a91b75", null, "Administrator", "ADMINISTRATOR" },
                    { "f612f68e-e251-4b99-935a-dca54fe4aa96", null, "Officer", "OFFICER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LicensePlate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5f3a2a92-9ce1-481e-bdce-0a8c6f7ac4c5", 0, "966d87c8-805c-420d-ab59-31544316482f", "user3@test.com", true, "User3", "Lastname", "USR323", false, null, "USER3@TEST.COM", "USER3@TEST.COM", "AQAAAAIAAYagAAAAEOgXgF3aFS5gpOdTbw+CI0gT8D9cgSp+hnhF2ggXTaNLIKevkUstZrNtJK+Gl2M42Q==", null, false, "", false, "user3@test.com" },
                    { "8ab473bd-7a14-42cb-b7eb-03ba984c159e", 0, "f1f0a761-7fdd-4977-a5f0-686c54a84501", "user2@test.com", true, "User2", "Lastname", "USR223", false, null, "USER2@TEST.COM", "USER2@TEST.COM", "AQAAAAIAAYagAAAAEOgXgF3aFS5gpOdTbw+CI0gT8D9cgSp+hnhF2ggXTaNLIKevkUstZrNtJK+Gl2M42Q==", null, false, "", false, "user2@test.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "93ff95e8-0f2e-422a-a966-240284a91b75", "5f3a2a92-9ce1-481e-bdce-0a8c6f7ac4c5" },
                    { "f612f68e-e251-4b99-935a-dca54fe4aa96", "5f3a2a92-9ce1-481e-bdce-0a8c6f7ac4c5" },
                    { "75dab2d3-add7-448c-b410-632f0c182751", "8ab473bd-7a14-42cb-b7eb-03ba984c159e" },
                    { "f612f68e-e251-4b99-935a-dca54fe4aa96", "8ab473bd-7a14-42cb-b7eb-03ba984c159e" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "93ff95e8-0f2e-422a-a966-240284a91b75", "5f3a2a92-9ce1-481e-bdce-0a8c6f7ac4c5" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f612f68e-e251-4b99-935a-dca54fe4aa96", "5f3a2a92-9ce1-481e-bdce-0a8c6f7ac4c5" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "75dab2d3-add7-448c-b410-632f0c182751", "8ab473bd-7a14-42cb-b7eb-03ba984c159e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f612f68e-e251-4b99-935a-dca54fe4aa96", "8ab473bd-7a14-42cb-b7eb-03ba984c159e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75dab2d3-add7-448c-b410-632f0c182751");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93ff95e8-0f2e-422a-a966-240284a91b75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f612f68e-e251-4b99-935a-dca54fe4aa96");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f3a2a92-9ce1-481e-bdce-0a8c6f7ac4c5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8ab473bd-7a14-42cb-b7eb-03ba984c159e");
        }
    }
}

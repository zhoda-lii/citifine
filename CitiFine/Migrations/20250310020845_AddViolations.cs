using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CitiFine.Migrations
{
    /// <inheritdoc />
    public partial class AddViolations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Violations",
                columns: table => new
                {
                    ViolationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViolationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FineAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violations", x => x.ViolationId);
                    table.ForeignKey(
                        name: "FK_Violations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07aed381-55f9-40e7-8af8-5ac360eaa7fa", null, "Officer", "OFFICER" },
                    { "19a2584c-c132-426f-9a7a-b6d5f9ba545a", null, "Administrator", "ADMINISTRATOR" },
                    { "1ea4a065-1b16-4754-bb3f-caf4916af4f3", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LicensePlate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "c401f890-4c9d-4da8-ad26-6c8a4f9d6532", 0, "c1328eef-0678-4138-bcaf-27dee400925c", "user2@test.com", true, "User2", "Lastname", "USR223", false, null, "USER2@TEST.COM", "USER2@TEST.COM", "AQAAAAIAAYagAAAAEE8VV2oIpTnOKUTgqHGb3XoPW+Mg0zshIwDtScjcMg7pcbLVUGypz6t7vOjtuKO5MQ==", null, false, "", false, "user2@test.com" },
                    { "dda06b3b-6a5b-4f27-ae9c-6b6f9c055923", 0, "a337dcca-4c47-4c33-8663-f2ed9f31e7f1", "user3@test.com", true, "User3", "Lastname", "USR323", false, null, "USER3@TEST.COM", "USER3@TEST.COM", "AQAAAAIAAYagAAAAEE8VV2oIpTnOKUTgqHGb3XoPW+Mg0zshIwDtScjcMg7pcbLVUGypz6t7vOjtuKO5MQ==", null, false, "", false, "user3@test.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "07aed381-55f9-40e7-8af8-5ac360eaa7fa", "c401f890-4c9d-4da8-ad26-6c8a4f9d6532" },
                    { "1ea4a065-1b16-4754-bb3f-caf4916af4f3", "c401f890-4c9d-4da8-ad26-6c8a4f9d6532" },
                    { "07aed381-55f9-40e7-8af8-5ac360eaa7fa", "dda06b3b-6a5b-4f27-ae9c-6b6f9c055923" },
                    { "19a2584c-c132-426f-9a7a-b6d5f9ba545a", "dda06b3b-6a5b-4f27-ae9c-6b6f9c055923" }
                });

            migrationBuilder.InsertData(
                table: "Violations",
                columns: new[] { "ViolationId", "DateIssued", "FineAmount", "IsPaid", "UserId", "ViolationType" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100.00m, false, "c401f890-4c9d-4da8-ad26-6c8a4f9d6532", "Speeding" },
                    { 2, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 150.00m, false, "c401f890-4c9d-4da8-ad26-6c8a4f9d6532", "Red Light" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Violations_UserId",
                table: "Violations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Violations");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "07aed381-55f9-40e7-8af8-5ac360eaa7fa", "c401f890-4c9d-4da8-ad26-6c8a4f9d6532" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1ea4a065-1b16-4754-bb3f-caf4916af4f3", "c401f890-4c9d-4da8-ad26-6c8a4f9d6532" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "07aed381-55f9-40e7-8af8-5ac360eaa7fa", "dda06b3b-6a5b-4f27-ae9c-6b6f9c055923" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "19a2584c-c132-426f-9a7a-b6d5f9ba545a", "dda06b3b-6a5b-4f27-ae9c-6b6f9c055923" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07aed381-55f9-40e7-8af8-5ac360eaa7fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19a2584c-c132-426f-9a7a-b6d5f9ba545a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ea4a065-1b16-4754-bb3f-caf4916af4f3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c401f890-4c9d-4da8-ad26-6c8a4f9d6532");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dda06b3b-6a5b-4f27-ae9c-6b6f9c055923");

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
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserAndRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2EC3020A-3C29-4BB3-AEC6-68733E6485CB", "b5a93e8a-92fa-4d7d-b50b-835e2bb7d276", "Admin", "ADMIN" },
                    { "EB29ACBA-8D33-400A-95FD-ED32114DF66E", "3f99faca-680f-4470-9be7-0aab15a3645a", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "B675C141-A780-4571-B5FB-333366B84D77", 0, "97148b43-c09e-4397-bc84-733fe74f2cdf", "admin@bookstore.com", false, "system", "admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAENXYaQtysxO7HJt+vs9o1VjNSjehqOHbKHZ3z1cAQqog8en7kBIw4b5saNdm2sCeSA==", null, false, "671fa3c0-3170-4935-bd2b-57604287c9ea", false, "admin@bookstore.com" },
                    { "E57CB190-4DBE-433A-8A1D-F5C9FCE96663", 0, "8e1a9d6a-74ad-4ffd-8fa2-0de7361b2e45", "user@bookstore.com", false, "system", "user", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAENCztDFs+JuwTT0JW/d6Jj1wfz3pVFWOETvIRkqISzG9sX7hwSp7KCMrmYx3Oat2CA==", null, false, "cd4de9f0-bc3c-47ee-92b2-ba03c491a6d2", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2EC3020A-3C29-4BB3-AEC6-68733E6485CB", "B675C141-A780-4571-B5FB-333366B84D77" },
                    { "EB29ACBA-8D33-400A-95FD-ED32114DF66E", "E57CB190-4DBE-433A-8A1D-F5C9FCE96663" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2EC3020A-3C29-4BB3-AEC6-68733E6485CB", "B675C141-A780-4571-B5FB-333366B84D77" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "EB29ACBA-8D33-400A-95FD-ED32114DF66E", "E57CB190-4DBE-433A-8A1D-F5C9FCE96663" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2EC3020A-3C29-4BB3-AEC6-68733E6485CB");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EB29ACBA-8D33-400A-95FD-ED32114DF66E");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B675C141-A780-4571-B5FB-333366B84D77");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "E57CB190-4DBE-433A-8A1D-F5C9FCE96663");
        }
    }
}

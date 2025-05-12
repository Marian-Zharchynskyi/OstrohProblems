using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "Administrator", "Administrator" },
                    { "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password_hash" },
                values: new object[,]
                {
                    { new Guid("0d3acedb-d84d-48e3-a76c-526921528eee"), "admin@example.com", "admin", "y5wSZ4BUzXH0urqYFjwSKg==:VIxLwbbbSOXSqoc0hZ6dh8BFnbJEW70H9kHZXmXsz/U=" },
                    { new Guid("5a670559-1d25-42f7-8dea-f586f7b32125"), "user@example.com", "user", "hW0EXv3JN7Yf/R6DQx3x+Q==:uruM6+soLp1MDDus/qbQHRU/CDvVDIqGYYMZCwQs6nE=" }
                });

            migrationBuilder.InsertData(
                table: "fk_user_roles",
                columns: new[] { "roles_id", "users_id" },
                values: new object[,]
                {
                    { "Administrator", new Guid("0d3acedb-d84d-48e3-a76c-526921528eee") },
                    { "User", new Guid("5a670559-1d25-42f7-8dea-f586f7b32125") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { "Administrator", new Guid("0d3acedb-d84d-48e3-a76c-526921528eee") });

            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { "User", new Guid("5a670559-1d25-42f7-8dea-f586f7b32125") });

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "Administrator");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "User");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("0d3acedb-d84d-48e3-a76c-526921528eee"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("5a670559-1d25-42f7-8dea-f586f7b32125"));
        }
    }
}

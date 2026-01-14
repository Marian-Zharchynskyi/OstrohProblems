using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddClerkIdToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "clerk_id",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "clerk_id",
                value: "user_38GDF8VSzp5vQGWOBgk0Kv3Td7W");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                column: "clerk_id",
                value: "user_38GDs4xMAfETf7thv21MwyW72bh");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                column: "clerk_id",
                value: "user_38GDlYzZ1LZNY3q4uFLBMyAYp4Y");

            migrationBuilder.CreateIndex(
                name: "ix_users_clerk_id",
                table: "users",
                column: "clerk_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_clerk_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "clerk_id",
                table: "users");
        }
    }
}

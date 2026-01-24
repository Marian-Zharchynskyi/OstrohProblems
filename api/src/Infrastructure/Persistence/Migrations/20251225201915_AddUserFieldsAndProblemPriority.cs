using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFieldsAndProblemPriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "full_name",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "surname",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "priority",
                table: "problems",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000001"),
                column: "priority",
                value: "Високий");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000002"),
                column: "priority",
                value: "Середній");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000003"),
                column: "priority",
                value: "Високий");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000004"),
                column: "priority",
                value: "Критичний");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000005"),
                column: "priority",
                value: "Високий");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000006"),
                column: "priority",
                value: "Середній");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000007"),
                column: "priority",
                value: "Високий");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000008"),
                column: "priority",
                value: "Критичний");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000009"),
                column: "priority",
                value: "Низький");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000010"),
                column: "priority",
                value: "Критичний");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "name", "phone_number", "surname" },
                values: new object[] { "Адміністратор", null, "Острога" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "name", "phone_number", "surname" },
                values: new object[] { "Звичайний", null, "Користувач" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "name", "phone_number", "surname" },
                values: new object[] { "Координатор", null, "Острога" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "users");

            migrationBuilder.DropColumn(
                name: "surname",
                table: "users");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "problems");

            migrationBuilder.AddColumn<string>(
                name: "full_name",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "full_name",
                value: "Адміністратор Острога");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                column: "full_name",
                value: "Звичайний Користувач");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                column: "full_name",
                value: "Координатор Острога");
        }
    }
}

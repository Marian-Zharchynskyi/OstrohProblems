using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserConfirmationStatusValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "user_confirmation_status",
                table: "problems",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000301"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000302"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000303"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000304"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000305"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000306"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000307"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000308"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000309"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030a"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030b"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030c"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030d"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030e"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030f"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000310"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000311"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000312"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000313"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000314"),
                column: "user_confirmation_status",
                value: "Очікує підтвердження");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "user_confirmation_status",
                table: "problems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000301"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000302"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000303"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000304"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000305"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000306"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000307"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000308"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000309"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030a"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030b"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030c"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030d"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030e"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030f"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000310"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000311"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000312"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000313"),
                column: "user_confirmation_status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000314"),
                column: "user_confirmation_status",
                value: 0);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProblemStatusValueObjectMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_problems_statuses_status_id",
                table: "problems");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropIndex(
                name: "ix_problems_status_id",
                table: "problems");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "problems");

            migrationBuilder.AddColumn<string>(
                name: "current_state",
                table: "problems",
                type: "varchar(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "problems",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000301"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Нова" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000302"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "В роботі" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000303"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Нова" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000304"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Виконано" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000305"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "В роботі" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000306"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Нова" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000307"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Нова" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000308"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Відхилено" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000309"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "В роботі" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030a"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Нова" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030b"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "В роботі" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030c"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Нова" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030d"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "В роботі" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030e"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Нова" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030f"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Виконано" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000310"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Нова" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000311"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Нова" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000312"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "В роботі" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000313"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Нова" });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000314"),
                columns: new[] { "current_state", "status" },
                values: new object[] { null, "Виконано" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_state",
                table: "problems");

            migrationBuilder.DropColumn(
                name: "status",
                table: "problems");

            migrationBuilder.AddColumn<Guid>(
                name: "status_id",
                table: "problems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_statuses", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000301"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000302"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000202"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000303"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000304"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000203"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000305"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000202"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000306"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000307"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000308"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000204"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000309"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000202"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030a"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030b"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000202"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030c"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030d"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000202"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030e"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030f"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000203"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000310"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000311"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000312"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000202"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000313"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.UpdateData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000314"),
                column: "status_id",
                value: new Guid("00000000-0000-0000-0000-000000000203"));

            migrationBuilder.InsertData(
                table: "statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000201"), "Нова" },
                    { new Guid("00000000-0000-0000-0000-000000000202"), "В роботі" },
                    { new Guid("00000000-0000-0000-0000-000000000203"), "Виконано" },
                    { new Guid("00000000-0000-0000-0000-000000000204"), "Відхилено" },
                    { new Guid("00000000-0000-0000-0000-000000000205"), "Потребує уточнення" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_problems_status_id",
                table: "problems",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "fk_problems_statuses_status_id",
                table: "problems",
                column: "status_id",
                principalTable: "statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

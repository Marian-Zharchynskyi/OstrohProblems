using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserSingleRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fk_user_roles");

            migrationBuilder.AddColumn<Guid>(
                name: "role_id",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "problems",
                columns: new[] { "id", "categories", "coordinator_comment", "coordinator_id", "created_at", "created_by_id", "current_state", "description", "latitude", "longitude", "rejection_reason", "status", "title", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0001-000000000001"), "[\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Велика яма на дорозі біля будинку №15. Потребує термінового ремонту.", 50.3294, 26.514399999999998, null, "Провалідована", "Розбита дорога на вул. Академічна", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000002"), "[\"\\u041E\\u0441\\u0432\\u0456\\u0442\\u043B\\u0435\\u043D\\u043D\\u044F\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Вже тиждень не світять ліхтарі на ділянці від будинку №5 до №15.", 50.328499999999998, 26.512499999999999, null, "Провалідована", "Не працює вуличне освітлення на вул. Семінарська", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000003"), "[\"\\u0421\\u043C\\u0456\\u0442\\u0442\\u044F\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Сміттєві контейнери не вивозяться вже 3 дні, сміття розкидане навколо.", 50.330100000000002, 26.5167, null, "Провалідована", "Переповнені сміттєві баки біля ринку", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000004"), "[\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\",\"\\u041F\\u0430\\u0440\\u043A\\u0438 \\u0442\\u0430 \\u0437\\u0435\\u043B\\u0435\\u043D\\u0456 \\u0437\\u043E\\u043D\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Старе дерево нахилилося і може впасти на дорогу. Небезпечно для пішоходів.", 50.331200000000003, 26.509799999999998, null, "В роботі", "Аварійне дерево на вул. Луцька", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000005"), "[\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\",\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Пішохідний перехід біля школи №2 без розмітки, небезпечно для дітей.", 50.328899999999997, 26.515599999999999, null, "В роботі", "Відсутня розмітка на пішохідному переході", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000006"), "[\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Великі тріщини на тротуарі, небезпечно для пішоходів та велосипедистів.", 50.330500000000001, 26.513400000000001, null, "Провалідована", "Тріщина на тротуарі вул. Папаніна", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000007"), "[\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\",\"\\u041F\\u0430\\u0440\\u043A\\u0438 \\u0442\\u0430 \\u0437\\u0435\\u043B\\u0435\\u043D\\u0456 \\u0437\\u043E\\u043D\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Гойдалки та гірка на дитячому майданчику в аварійному стані.", 50.328200000000002, 26.514199999999999, null, "В роботі", "Потребує ремонту дитячий майданчик", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000008"), "[\"\\u0412\\u043E\\u0434\\u043E\\u043F\\u043E\\u0441\\u0442\\u0430\\u0447\\u0430\\u043D\\u043D\\u044F\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Витік води з водопровідної труби, вода заливає дорогу.", 50.329099999999997, 26.517099999999999, null, "Нова", "Прорив водопроводу на вул. Замкова", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000009"), "[\"\\u041F\\u0430\\u0440\\u043A\\u0438 \\u0442\\u0430 \\u0437\\u0435\\u043B\\u0435\\u043D\\u0456 \\u0437\\u043E\\u043D\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Клумби біля центральної площі не доглядаються, заросли бур'яном.", 50.329599999999999, 26.514900000000001, null, "Нова", "Зарослі бур'яном клумби в центрі", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000010"), "[\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\",\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Світлофор на перехресті не працює вже другий день, створює аварійну ситуацію.", 50.331499999999998, 26.510200000000001, null, "Відхилено", "Не працює світлофор на вул. Луцька", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "role_id",
                value: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                column: "role_id",
                value: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                column: "role_id",
                value: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.InsertData(
                table: "comments",
                columns: new[] { "id", "content", "created_at", "problem_id", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0002-000000000001"), "Підтверджую, яма дуже глибока, пошкодив колесо.", new DateTime(2024, 1, 16, 15, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0001-000000000001"), new DateTime(2024, 1, 16, 15, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0002-000000000002"), "Дякуємо за звернення, передали в дорожню службу.", new DateTime(2024, 1, 16, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0001-000000000001"), new DateTime(2024, 1, 16, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-0000-0000-0002-000000000003"), "Темно ввечері ходити, небезпечно.", new DateTime(2024, 1, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0001-000000000002"), new DateTime(2024, 1, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0002-000000000004"), "Жахливий запах, неможливо пройти повз.", new DateTime(2024, 1, 16, 18, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0001-000000000003"), new DateTime(2024, 1, 16, 18, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0002-000000000005"), "Прошу терміново вирішити, гілки вже торкаються проводів.", new DateTime(2024, 1, 16, 19, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0001-000000000004"), new DateTime(2024, 1, 16, 19, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0002-000000000006"), "Діти переходять дорогу в небезпечному місці.", new DateTime(2024, 1, 16, 20, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0001-000000000005"), new DateTime(2024, 1, 16, 20, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0002-000000000007"), "Вода тече вже третій день, ніхто не реагує.", new DateTime(2024, 1, 16, 21, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0001-000000000008"), new DateTime(2024, 1, 16, 21, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011") }
                });

            migrationBuilder.InsertData(
                table: "ratings",
                columns: new[] { "id", "created_at", "points", "problem_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0003-000000000001"), new DateTime(2024, 1, 17, 11, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0003-000000000002"), new DateTime(2024, 1, 17, 12, 0, 0, 0, DateTimeKind.Utc), 4.0, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0003-000000000003"), new DateTime(2024, 1, 17, 13, 0, 0, 0, DateTimeKind.Utc), 4.0, new Guid("00000000-0000-0000-0001-000000000002"), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0003-000000000004"), new DateTime(2024, 1, 17, 14, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("00000000-0000-0000-0001-000000000003"), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0003-000000000005"), new DateTime(2024, 1, 17, 15, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("00000000-0000-0000-0001-000000000004"), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0003-000000000006"), new DateTime(2024, 1, 17, 16, 0, 0, 0, DateTimeKind.Utc), 4.0, new Guid("00000000-0000-0000-0001-000000000005"), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0003-000000000007"), new DateTime(2024, 1, 17, 17, 0, 0, 0, DateTimeKind.Utc), 3.0, new Guid("00000000-0000-0000-0001-000000000006"), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0003-000000000008"), new DateTime(2024, 1, 17, 18, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("00000000-0000-0000-0001-000000000008"), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0003-000000000009"), new DateTime(2024, 1, 17, 19, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("00000000-0000-0000-0001-000000000010"), new Guid("00000000-0000-0000-0000-000000000011") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_roles_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_role_id",
                table: "users");

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0002-000000000001"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0002-000000000003"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0002-000000000004"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0002-000000000005"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0002-000000000006"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0002-000000000007"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000007"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000009"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0003-000000000001"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0003-000000000002"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0003-000000000004"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0003-000000000005"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0003-000000000006"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0003-000000000007"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0003-000000000008"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0003-000000000009"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000002"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000003"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000004"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000005"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000006"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000008"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0001-000000000010"));

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "users");

            migrationBuilder.CreateTable(
                name: "fk_user_roles",
                columns: table => new
                {
                    roles_id = table.Column<Guid>(type: "uuid", nullable: false),
                    users_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fk_user_roles", x => new { x.roles_id, x.users_id });
                    table.ForeignKey(
                        name: "fk_fk_user_roles_roles_roles_id",
                        column: x => x.roles_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fk_user_roles_users_users_id",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "fk_user_roles",
                columns: new[] { "roles_id", "users_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000012") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_fk_user_roles_users_id",
                table: "fk_user_roles",
                column: "users_id");
        }
    }
}

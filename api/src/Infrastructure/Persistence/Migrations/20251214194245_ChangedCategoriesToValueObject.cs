using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCategoriesToValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_coordinator_images_problem_id",
                table: "coordinator_image");

            migrationBuilder.DropTable(
                name: "fk_problem_categories");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000401"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000402"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000403"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000404"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000405"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000406"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000407"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000408"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000409"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000040a"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000308"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030c"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030d"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030f"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000310"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000312"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000313"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000314"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000501"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000502"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000503"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000504"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000505"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000506"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000507"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000508"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000509"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000050a"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000301"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000302"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000303"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000304"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000305"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000306"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000307"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000309"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030a"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030b"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030e"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000311"));

            migrationBuilder.AddColumn<string>(
                name: "categories",
                table: "problems",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "fk_problem_coordinator_images_id",
                table: "coordinator_image",
                column: "problem_id",
                principalTable: "problems",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_problem_coordinator_images_id",
                table: "coordinator_image");

            migrationBuilder.DropColumn(
                name: "categories",
                table: "problems");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fk_problem_categories",
                columns: table => new
                {
                    categories_id = table.Column<Guid>(type: "uuid", nullable: false),
                    problems_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fk_problem_categories", x => new { x.categories_id, x.problems_id });
                    table.ForeignKey(
                        name: "fk_fk_problem_categories_categories_categories_id",
                        column: x => x.categories_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fk_problem_categories_problems_problems_id",
                        column: x => x.problems_id,
                        principalTable: "problems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000101"), "Дороги та тротуари" },
                    { new Guid("00000000-0000-0000-0000-000000000102"), "Освітлення" },
                    { new Guid("00000000-0000-0000-0000-000000000103"), "Благоустрій" },
                    { new Guid("00000000-0000-0000-0000-000000000104"), "Сміття та екологія" },
                    { new Guid("00000000-0000-0000-0000-000000000105"), "Комунальні послуги" },
                    { new Guid("00000000-0000-0000-0000-000000000106"), "Транспорт" },
                    { new Guid("00000000-0000-0000-0000-000000000107"), "Безпека" },
                    { new Guid("00000000-0000-0000-0000-000000000108"), "Парки та зелені зони" },
                    { new Guid("00000000-0000-0000-0000-000000000109"), "Будівництво" },
                    { new Guid("00000000-0000-0000-0000-00000000010a"), "Інше" }
                });

            migrationBuilder.InsertData(
                table: "problems",
                columns: new[] { "id", "coordinator_comment", "coordinator_id", "created_at", "created_by_id", "current_state", "description", "latitude", "longitude", "rejection_reason", "status", "title", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000301"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Велика яма на дорозі біля будинку №15. Потрібує термінового ремонту.", 50.3294, 26.514399999999998, null, "Нова", "Розбита дорога на вул. Академічна", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000302"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Вже тиждень не світять ліхтарі на ділянці від будинку №10 до №20.", 50.328499999999998, 26.512499999999999, null, "В роботі", "Не працює вуличне освітлення на вул. Семінарська", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000303"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Сміттєві контейнери не вивозяться вже 3 дні, сміття розкидане навколо.", 50.330100000000002, 26.5167, null, "Нова", "Переповнені сміттєві баки біля ринку", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000304"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Дерев'яна лавка зламана, потребує заміни або ремонту.", 50.327800000000003, 26.518899999999999, null, "Виконано", "Зламана лавка в парку Тараса Шевченка", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000305"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Старе дерево нахилилося і може впасти на дорогу. Небезпечно для пішоходів та транспорту.", 50.331200000000003, 26.509799999999998, null, "В роботі", "Аварійне дерево на вул. Луцька", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000306"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Пішохідний перехід біля школи №2 без розмітки, небезпечно для дітей.", 50.328899999999997, 26.515599999999999, null, "Нова", "Відсутня розмітка на пішохідному переході", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000307"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Великі тріщини на тротуарі, небезпечно для пішоходів, особливо в темний час доби.", 50.330500000000001, 26.513400000000001, null, "Нова", "Тріщина на тротуарі вул. Папаніна", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000308"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Ведеться будівництво без відповідних дозволів, порушується архітектурний вигляд міста.", 50.329799999999999, 26.517800000000001, null, "Відхилено", "Незаконне будівництво на вул. Князів Острозьких", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000309"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Гойдалки та гірка на дитячому майданчику в аварійному стані.", 50.328200000000002, 26.514199999999999, null, "В роботі", "Потребує ремонту дитячий майданчик", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-00000000030a"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "На перехресті вул. Академічна та вул. Семінарська відсутній знак пріоритету.", 50.3307, 26.516100000000002, null, "Нова", "Відсутній дорожній знак на перехресті", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-00000000030b"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Витік води з водопровідної труби, вода заливає дорогу.", 50.329099999999997, 26.517099999999999, null, "В роботі", "Прорив водопроводу на вул. Замкова", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-00000000030c"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Клумби біля центральної площі не доглядаються, заросли бур'яном.", 50.329599999999999, 26.514900000000001, null, "Нова", "Зарослі бур'яном клумби в центрі", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-00000000030d"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Світлофор на перехресті не працює вже другий день.", 50.331499999999998, 26.510200000000001, null, "В роботі", "Не працює світлофор на вул. Луцька", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-00000000030e"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Стихійне сміттєзвалище утворилося в лісосмузі за містом.", 50.327199999999998, 26.519500000000001, null, "Нова", "Сміттєзвалище в лісосмузі", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-00000000030f"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Огорожа школи №1 іржава та потребує фарбування.", 50.328800000000001, 26.5138, null, "Виконано", "Потребує фарбування огорожа школи", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000310"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "На автобусній зупинці 'Центр' немає урн для сміття.", 50.330199999999998, 26.5153, null, "Нова", "Відсутні урни на зупинці", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000311"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Бордюр зруйнований на протяжності 5 метрів.", 50.329900000000002, 26.516400000000001, null, "Нова", "Зламаний бордюр на вул. Князів Острозьких", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000312"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Гілки дерева загрожують електричним проводам.", 50.328600000000002, 26.514700000000001, null, "В роботі", "Потребує обрізки дерева біля будинку", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000313"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Після дощу утворюються великі калюжі через відсутність каналізації.", 50.3279, 26.513200000000001, null, "Нова", "Відсутня каналізація на вул. Садова", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000314"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), null, "Вандали розмалювали фасад історичної будівлі в центрі міста.", 50.329300000000003, 26.517499999999998, null, "Виконано", "Граффіті на фасаді історичної будівлі", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "comments",
                columns: new[] { "id", "content", "created_at", "problem_id", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000401"), "Так, підтверджую. Їздив сьогодні, ледь не зламав підвіску.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000301"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000402"), "Вже місяць як така ситуація. Коли вже відремонтують?", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000301"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000403"), "Дуже небезпечно ходити ввечері, треба терміново виправити.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000302"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000404"), "Жахлива ситуація, смердить на всю вулицю.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000303"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000405"), "Дякую, що виправили!", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000304"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000406"), "Дійсно небезпечно, особливо при сильному вітрі.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000305"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000407"), "Діти щодня переходять дорогу, це дуже небезпечно!", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000306"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000408"), "Коли почнуть ремонт? Діти не можуть нормально гратися.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000309"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000409"), "Вода вже затопила половину дороги!", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-00000000030b"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-00000000040a"), "Це екологічна катастрофа для нашого міста!", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-00000000030e"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010") }
                });

            migrationBuilder.InsertData(
                table: "fk_problem_categories",
                columns: new[] { "categories_id", "problems_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000101"), new Guid("00000000-0000-0000-0000-000000000301") },
                    { new Guid("00000000-0000-0000-0000-000000000101"), new Guid("00000000-0000-0000-0000-000000000306") },
                    { new Guid("00000000-0000-0000-0000-000000000101"), new Guid("00000000-0000-0000-0000-000000000307") },
                    { new Guid("00000000-0000-0000-0000-000000000101"), new Guid("00000000-0000-0000-0000-000000000311") },
                    { new Guid("00000000-0000-0000-0000-000000000102"), new Guid("00000000-0000-0000-0000-000000000302") },
                    { new Guid("00000000-0000-0000-0000-000000000103"), new Guid("00000000-0000-0000-0000-000000000304") },
                    { new Guid("00000000-0000-0000-0000-000000000103"), new Guid("00000000-0000-0000-0000-000000000309") },
                    { new Guid("00000000-0000-0000-0000-000000000103"), new Guid("00000000-0000-0000-0000-00000000030c") },
                    { new Guid("00000000-0000-0000-0000-000000000103"), new Guid("00000000-0000-0000-0000-00000000030f") },
                    { new Guid("00000000-0000-0000-0000-000000000103"), new Guid("00000000-0000-0000-0000-000000000314") },
                    { new Guid("00000000-0000-0000-0000-000000000104"), new Guid("00000000-0000-0000-0000-000000000303") },
                    { new Guid("00000000-0000-0000-0000-000000000104"), new Guid("00000000-0000-0000-0000-00000000030e") },
                    { new Guid("00000000-0000-0000-0000-000000000104"), new Guid("00000000-0000-0000-0000-000000000310") },
                    { new Guid("00000000-0000-0000-0000-000000000105"), new Guid("00000000-0000-0000-0000-00000000030b") },
                    { new Guid("00000000-0000-0000-0000-000000000105"), new Guid("00000000-0000-0000-0000-000000000313") },
                    { new Guid("00000000-0000-0000-0000-000000000106"), new Guid("00000000-0000-0000-0000-00000000030a") },
                    { new Guid("00000000-0000-0000-0000-000000000106"), new Guid("00000000-0000-0000-0000-00000000030d") },
                    { new Guid("00000000-0000-0000-0000-000000000106"), new Guid("00000000-0000-0000-0000-000000000310") },
                    { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-000000000305") },
                    { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-000000000306") },
                    { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-000000000309") },
                    { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-00000000030a") },
                    { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-00000000030d") },
                    { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-000000000312") },
                    { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-000000000314") },
                    { new Guid("00000000-0000-0000-0000-000000000108"), new Guid("00000000-0000-0000-0000-000000000304") },
                    { new Guid("00000000-0000-0000-0000-000000000108"), new Guid("00000000-0000-0000-0000-000000000305") },
                    { new Guid("00000000-0000-0000-0000-000000000108"), new Guid("00000000-0000-0000-0000-00000000030c") },
                    { new Guid("00000000-0000-0000-0000-000000000108"), new Guid("00000000-0000-0000-0000-000000000312") },
                    { new Guid("00000000-0000-0000-0000-000000000109"), new Guid("00000000-0000-0000-0000-000000000308") }
                });

            migrationBuilder.InsertData(
                table: "ratings",
                columns: new[] { "id", "created_at", "points", "problem_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000501"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4.5, new Guid("00000000-0000-0000-0000-000000000301"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000502"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("00000000-0000-0000-0000-000000000302"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000503"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4.7999999999999998, new Guid("00000000-0000-0000-0000-000000000303"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000504"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("00000000-0000-0000-0000-000000000305"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000505"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4.9000000000000004, new Guid("00000000-0000-0000-0000-000000000306"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000506"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4.2000000000000002, new Guid("00000000-0000-0000-0000-000000000307"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000507"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4.7000000000000002, new Guid("00000000-0000-0000-0000-00000000030a"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000508"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("00000000-0000-0000-0000-00000000030b"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000509"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4.5999999999999996, new Guid("00000000-0000-0000-0000-00000000030e"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-00000000050a"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4.2999999999999998, new Guid("00000000-0000-0000-0000-000000000311"), new Guid("00000000-0000-0000-0000-000000000010") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_fk_problem_categories_problems_id",
                table: "fk_problem_categories",
                column: "problems_id");

            migrationBuilder.AddForeignKey(
                name: "fk_coordinator_images_problem_id",
                table: "coordinator_image",
                column: "problem_id",
                principalTable: "problems",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

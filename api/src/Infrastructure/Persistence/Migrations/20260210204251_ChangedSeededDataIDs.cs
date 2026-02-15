using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSeededDataIDs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("34a92614-7d43-41c6-9430-891a92543265"), "Administrator" },
                    { new Guid("42ad2488-8250-4286-921c-4395e1e19409"), "Coordinator" },
                    { new Guid("97897262-63fe-45f8-b7eb-661bf11f0c23"), "User" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "clerk_id", "email", "name", "password_hash", "phone_number", "role_id", "surname" },
                values: new object[,]
                {
                    { new Guid("7d0c1b2a-3e4f-5a6b-7c8d-9e0f1a2b3c4d"), "user_38GDlYzZ1LZNY3q4uFLBMyAYp4Y", "coordinator@ostroh.edu.ua", "Координатор", "DXIF+E53jMJS34YZkf0Jkw==:ccAPZCJbWy/stpuqDGYoAeNnHM5rABefB+bMZL+EaZY=", null, new Guid("42ad2488-8250-4286-921c-4395e1e19409"), "Острога" },
                    { new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"), "user_38GDs4xMAfETf7thv21MwyW72bh", "user@ostroh.edu.ua", "Звичайний", "OeuuWFIVglEfvDvcH349ow==:OXOk32vZFxUcodlJVPaLj/qOApIGP9SSVu9RBy+O4Sc=", null, new Guid("97897262-63fe-45f8-b7eb-661bf11f0c23"), "Користувач" },
                    { new Guid("9f2b8c3d-4a5e-6f7a-8b9c-0d1e2f3a4b5c"), "user_38GDF8VSzp5vQGWOBgk0Kv3Td7W", "admin@ostroh.edu.ua", "Адміністратор", "31jGqnyNWeEpqqSOGrrFYA==:pt36JXYoIE3w8xtI8rEJU/h50muKgFwRs0p/h4am3A0=", null, new Guid("34a92614-7d43-41c6-9430-891a92543265"), "Острога" }
                });

            migrationBuilder.InsertData(
                table: "problems",
                columns: new[] { "id", "categories", "coordinator_comment", "coordinator_id", "created_at", "created_by_id", "current_state", "description", "latitude", "longitude", "priority", "rejection_reason", "status", "title", "updated_at" },
                values: new object[,]
                {
                    { new Guid("0a7b8c9d-0e1f-2a3b-4c5d-6e7f8a9b0c1d"), "[\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\",\"\\u041F\\u0430\\u0440\\u043A\\u0438 \\u0442\\u0430 \\u0437\\u0435\\u043B\\u0435\\u043D\\u0456 \\u0437\\u043E\\u043D\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"), null, "Гойдалки та гірка на дитячому майданчику в аварійному стані.", 50.328200000000002, 26.514199999999999, "Високий", null, "В роботі", "Потребує ремонту дитячий майданчик", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1f6a7b8c-9d0e-1f2a-3b4c-5d6e7f8a9b0c"), "[\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"), null, "Великі тріщини на тротуарі, небезпечно для пішоходів та велосипедистів.", 50.330500000000001, 26.513400000000001, "Середній", null, "Нова", "Тріщина на тротуарі вул. Папаніна", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2e5f6a7b-8c9d-0e1f-2a3b-4c5d6e7f8a9b"), "[\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\",\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"), null, "Пішохідний перехід біля школи №2 без розмітки, небезпечно для дітей.", 50.328899999999997, 26.515599999999999, "Високий", null, "В роботі", "Відсутня розмітка на пішохідному переході", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"), "[\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\",\"\\u041F\\u0430\\u0440\\u043A\\u0438 \\u0442\\u0430 \\u0437\\u0435\\u043B\\u0435\\u043D\\u0456 \\u0437\\u043E\\u043D\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"), null, "Старе дерево нахилилося і може впасти на дорогу. Небезпечно для пішоходів.", 50.331200000000003, 26.509799999999998, "Критичний", null, "В роботі", "Аварійне дерево на вул. Луцька", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"), "[\"\\u0421\\u043C\\u0456\\u0442\\u0442\\u044F\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"), null, "Сміттєві контейнери не вивозяться вже 3 дні, сміття розкидане навколо.", 50.330100000000002, 26.5167, "Високий", null, "Нова", "Переповнені сміттєві баки біля ринку", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"), "[\"\\u041E\\u0441\\u0432\\u0456\\u0442\\u043B\\u0435\\u043D\\u043D\\u044F\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"), null, "Вже тиждень не світять ліхтарі на ділянці від будинку №5 до №15.", 50.328499999999998, 26.512499999999999, "Середній", null, "Нова", "Не працює вуличне освітлення на вул. Семінарська", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"), "[\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"), null, "Велика яма на дорозі біля будинку №15. Потребує термінового ремонту.", 50.3294, 26.514399999999998, "Високий", null, "Нова", "Розбита дорога на вул. Академічна", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7d0e1f2a-3b4c-5d6e-7f8a-9b0c1d2e3f4a"), "[\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\",\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"), null, "Світлофор на перехресті не працює вже другий день, створює аварійну ситуацію.", 50.331499999999998, 26.510200000000001, "Критичний", null, "Відхилено", "Не працює світлофор на вул. Луцька", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8c9d0e1f-2a3b-4c5d-6e7f-8a9b0c1d2e3f"), "[\"\\u041F\\u0430\\u0440\\u043A\\u0438 \\u0442\\u0430 \\u0437\\u0435\\u043B\\u0435\\u043D\\u0456 \\u0437\\u043E\\u043D\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"), null, "Клумби біля центральної площі не доглядаються, заросли бур'яном.", 50.329599999999999, 26.514900000000001, "Низький", null, "Нова", "Зарослі бур'яном клумби в центрі", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9b8c9d0e-1f2a-3b4c-5d6e-7f8a9b0c1d2e"), "[\"\\u0412\\u043E\\u0434\\u043E\\u043F\\u043E\\u0441\\u0442\\u0430\\u0447\\u0430\\u043D\\u043D\\u044F\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"), null, "Витік води з водопровідної труби, вода заливає дорогу.", 50.329099999999997, 26.517099999999999, "Критичний", null, "Нова", "Прорив водопроводу на вул. Замкова", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "comments",
                columns: new[] { "id", "content", "created_at", "problem_id", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("12345678-9abc-def0-1234-56789abcdef0"), "Вода тече вже третій день, ніхто не реагує.", new DateTime(2024, 1, 16, 21, 0, 0, 0, DateTimeKind.Utc), new Guid("9b8c9d0e-1f2a-3b4c-5d6e-7f8a9b0c1d2e"), new DateTime(2024, 1, 16, 21, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"), "Підтверджую, яма дуже глибока, пошкодив колесо.", new DateTime(2024, 1, 16, 15, 0, 0, 0, DateTimeKind.Utc), new Guid("6a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"), new DateTime(2024, 1, 16, 15, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e"), "Дякуємо за звернення, передали в дорожню службу.", new DateTime(2024, 1, 16, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("6a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"), new DateTime(2024, 1, 16, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("7d0c1b2a-3e4f-5a6b-7c8d-9e0f1a2b3c4d") },
                    { new Guid("c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f"), "Темно ввечері ходити, небезпечно.", new DateTime(2024, 1, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("5b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"), new DateTime(2024, 1, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"), "Жахливий запах, неможливо пройти повз.", new DateTime(2024, 1, 16, 18, 0, 0, 0, DateTimeKind.Utc), new Guid("4c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"), new DateTime(2024, 1, 16, 18, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("e5f6a7b8-c9d0-1e2f-3a4b-5c6d7e8f9a0b"), "Прошу терміново вирішити, гілки вже торкаються проводів.", new DateTime(2024, 1, 16, 19, 0, 0, 0, DateTimeKind.Utc), new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"), new DateTime(2024, 1, 16, 19, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("f6a7b8c9-d0e1-2f3a-4b5c-6d7e8f9a0b1c"), "Діти переходять дорогу в небезпечному місці.", new DateTime(2024, 1, 16, 20, 0, 0, 0, DateTimeKind.Utc), new Guid("2e5f6a7b-8c9d-0e1f-2a3b-4c5d6e7f8a9b"), new DateTime(2024, 1, 16, 20, 0, 0, 0, DateTimeKind.Utc), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") }
                });

            migrationBuilder.InsertData(
                table: "ratings",
                columns: new[] { "id", "created_at", "points", "problem_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("00e3e57e-07c8-4767-9610-639556214588"), new DateTime(2024, 1, 17, 17, 0, 0, 0, DateTimeKind.Utc), 3.0, new Guid("1f6a7b8c-9d0e-1f2a-3b4c-5d6e7f8a9b0c"), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("11e3e57e-07c8-4767-9610-639556214589"), new DateTime(2024, 1, 17, 18, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("9b8c9d0e-1f2a-3b4c-5d6e-7f8a9b0c1d2e"), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("22e3e57e-07c8-4767-9610-639556214590"), new DateTime(2024, 1, 17, 19, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("7d0e1f2a-3b4c-5d6e-7f8a-9b0c1d2e3f4a"), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("ace3e57e-07c8-4767-9610-639556214582"), new DateTime(2024, 1, 17, 11, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("6a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("bde3e57e-07c8-4767-9610-639556214583"), new DateTime(2024, 1, 17, 12, 0, 0, 0, DateTimeKind.Utc), 4.0, new Guid("6a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"), new Guid("9f2b8c3d-4a5e-6f7a-8b9c-0d1e2f3a4b5c") },
                    { new Guid("cde3e57e-07c8-4767-9610-639556214584"), new DateTime(2024, 1, 17, 13, 0, 0, 0, DateTimeKind.Utc), 4.0, new Guid("5b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("dde3e57e-07c8-4767-9610-639556214585"), new DateTime(2024, 1, 17, 14, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("4c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("ede3e57e-07c8-4767-9610-639556214586"), new DateTime(2024, 1, 17, 15, 0, 0, 0, DateTimeKind.Utc), 5.0, new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") },
                    { new Guid("fde3e57e-07c8-4767-9610-639556214587"), new DateTime(2024, 1, 17, 16, 0, 0, 0, DateTimeKind.Utc), 4.0, new Guid("2e5f6a7b-8c9d-0e1f-2a3b-4c5d6e7f8a9b"), new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("12345678-9abc-def0-1234-56789abcdef0"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("e5f6a7b8-c9d0-1e2f-3a4b-5c6d7e8f9a0b"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("f6a7b8c9-d0e1-2f3a-4b5c-6d7e8f9a0b1c"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("0a7b8c9d-0e1f-2a3b-4c5d-6e7f8a9b0c1d"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("8c9d0e1f-2a3b-4c5d-6e7f-8a9b0c1d2e3f"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("00e3e57e-07c8-4767-9610-639556214588"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("11e3e57e-07c8-4767-9610-639556214589"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("22e3e57e-07c8-4767-9610-639556214590"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("ace3e57e-07c8-4767-9610-639556214582"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("bde3e57e-07c8-4767-9610-639556214583"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("cde3e57e-07c8-4767-9610-639556214584"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("dde3e57e-07c8-4767-9610-639556214585"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("ede3e57e-07c8-4767-9610-639556214586"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("fde3e57e-07c8-4767-9610-639556214587"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("1f6a7b8c-9d0e-1f2a-3b4c-5d6e7f8a9b0c"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("2e5f6a7b-8c9d-0e1f-2a3b-4c5d6e7f8a9b"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("4c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("5b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("6a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("7d0e1f2a-3b4c-5d6e-7f8a-9b0c1d2e3f4a"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("9b8c9d0e-1f2a-3b4c-5d6e-7f8a9b0c1d2e"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("7d0c1b2a-3e4f-5a6b-7c8d-9e0f1a2b3c4d"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("9f2b8c3d-4a5e-6f7a-8b9c-0d1e2f3a4b5c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("34a92614-7d43-41c6-9430-891a92543265"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("42ad2488-8250-4286-921c-4395e1e19409"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("8e1a2b3c-4d5e-6f7a-8b9c-0d1e2f3a4b5d"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("97897262-63fe-45f8-b7eb-661bf11f0c23"));

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Administrator" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "User" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Coordinator" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "clerk_id", "email", "name", "password_hash", "phone_number", "role_id", "surname" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000010"), "user_38GDF8VSzp5vQGWOBgk0Kv3Td7W", "admin@ostroh.edu.ua", "Адміністратор", "31jGqnyNWeEpqqSOGrrFYA==:pt36JXYoIE3w8xtI8rEJU/h50muKgFwRs0p/h4am3A0=", null, new Guid("00000000-0000-0000-0000-000000000001"), "Острога" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "user_38GDs4xMAfETf7thv21MwyW72bh", "user@ostroh.edu.ua", "Звичайний", "OeuuWFIVglEfvDvcH349ow==:OXOk32vZFxUcodlJVPaLj/qOApIGP9SSVu9RBy+O4Sc=", null, new Guid("00000000-0000-0000-0000-000000000002"), "Користувач" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "user_38GDlYzZ1LZNY3q4uFLBMyAYp4Y", "coordinator@ostroh.edu.ua", "Координатор", "DXIF+E53jMJS34YZkf0Jkw==:ccAPZCJbWy/stpuqDGYoAeNnHM5rABefB+bMZL+EaZY=", null, new Guid("00000000-0000-0000-0000-000000000003"), "Острога" }
                });

            migrationBuilder.InsertData(
                table: "problems",
                columns: new[] { "id", "categories", "coordinator_comment", "coordinator_id", "created_at", "created_by_id", "current_state", "description", "latitude", "longitude", "priority", "rejection_reason", "status", "title", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0001-000000000001"), "[\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Велика яма на дорозі біля будинку №15. Потребує термінового ремонту.", 50.3294, 26.514399999999998, "Високий", null, "Нова", "Розбита дорога на вул. Академічна", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000002"), "[\"\\u041E\\u0441\\u0432\\u0456\\u0442\\u043B\\u0435\\u043D\\u043D\\u044F\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Вже тиждень не світять ліхтарі на ділянці від будинку №5 до №15.", 50.328499999999998, 26.512499999999999, "Середній", null, "Нова", "Не працює вуличне освітлення на вул. Семінарська", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000003"), "[\"\\u0421\\u043C\\u0456\\u0442\\u0442\\u044F\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Сміттєві контейнери не вивозяться вже 3 дні, сміття розкидане навколо.", 50.330100000000002, 26.5167, "Високий", null, "Нова", "Переповнені сміттєві баки біля ринку", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000004"), "[\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\",\"\\u041F\\u0430\\u0440\\u043A\\u0438 \\u0442\\u0430 \\u0437\\u0435\\u043B\\u0435\\u043D\\u0456 \\u0437\\u043E\\u043D\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Старе дерево нахилилося і може впасти на дорогу. Небезпечно для пішоходів.", 50.331200000000003, 26.509799999999998, "Критичний", null, "В роботі", "Аварійне дерево на вул. Луцька", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000005"), "[\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\",\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Пішохідний перехід біля школи №2 без розмітки, небезпечно для дітей.", 50.328899999999997, 26.515599999999999, "Високий", null, "В роботі", "Відсутня розмітка на пішохідному переході", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000006"), "[\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Великі тріщини на тротуарі, небезпечно для пішоходів та велосипедистів.", 50.330500000000001, 26.513400000000001, "Середній", null, "Нова", "Тріщина на тротуарі вул. Папаніна", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000007"), "[\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\",\"\\u041F\\u0430\\u0440\\u043A\\u0438 \\u0442\\u0430 \\u0437\\u0435\\u043B\\u0435\\u043D\\u0456 \\u0437\\u043E\\u043D\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Гойдалки та гірка на дитячому майданчику в аварійному стані.", 50.328200000000002, 26.514199999999999, "Високий", null, "В роботі", "Потребує ремонту дитячий майданчик", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000008"), "[\"\\u0412\\u043E\\u0434\\u043E\\u043F\\u043E\\u0441\\u0442\\u0430\\u0447\\u0430\\u043D\\u043D\\u044F\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Витік води з водопровідної труби, вода заливає дорогу.", 50.329099999999997, 26.517099999999999, "Критичний", null, "Нова", "Прорив водопроводу на вул. Замкова", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000009"), "[\"\\u041F\\u0430\\u0440\\u043A\\u0438 \\u0442\\u0430 \\u0437\\u0435\\u043B\\u0435\\u043D\\u0456 \\u0437\\u043E\\u043D\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Клумби біля центральної площі не доглядаються, заросли бур'яном.", 50.329599999999999, 26.514900000000001, "Низький", null, "Нова", "Зарослі бур'яном клумби в центрі", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0001-000000000010"), "[\"\\u0411\\u0435\\u0437\\u043F\\u0435\\u043A\\u0430\",\"\\u0414\\u043E\\u0440\\u043E\\u0433\\u0438\"]", null, null, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), null, "Світлофор на перехресті не працює вже другий день, створює аварійну ситуацію.", 50.331499999999998, 26.510200000000001, "Критичний", null, "Відхилено", "Не працює світлофор на вул. Луцька", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) }
                });

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
        }
    }
}

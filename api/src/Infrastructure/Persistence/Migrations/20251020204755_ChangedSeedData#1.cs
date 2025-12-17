using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSeedData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("0fb15901-2556-4ad8-896a-5a83b79c55f2"), new Guid("3b5dc36c-258c-446d-98db-d4a733ac637b") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("0fb15901-2556-4ad8-896a-5a83b79c55f2"), new Guid("99b05525-4d4b-435e-85b2-b4a3ff8d46fb") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("1560a201-38d3-4581-892e-bcf57047d1b4"), new Guid("3b5dc36c-258c-446d-98db-d4a733ac637b") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("1560a201-38d3-4581-892e-bcf57047d1b4"), new Guid("7c717a3e-ccca-4e56-b4a5-36c83331deb4") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("3862dd30-1b4f-41f8-93e5-a33298e64ba4"), new Guid("45073a7c-2949-456e-97df-85f6109d7c88") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("3862dd30-1b4f-41f8-93e5-a33298e64ba4"), new Guid("f92a1ffd-6630-437a-9d41-3763fcb80fdd") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("3d10b203-858e-4e73-9e8a-6917d38fe2d5"), new Guid("99b05525-4d4b-435e-85b2-b4a3ff8d46fb") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("3d10b203-858e-4e73-9e8a-6917d38fe2d5"), new Guid("adc23850-b45e-413f-b2d1-a7aabca606cb") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("67ecbac2-70c1-4fef-8b61-10232266d9a3"), new Guid("45073a7c-2949-456e-97df-85f6109d7c88") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("67ecbac2-70c1-4fef-8b61-10232266d9a3"), new Guid("94ca64b7-b8f5-474f-92b1-eede5e5d30d4") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("69046331-69d0-4836-9a1d-b01eee942ecb"), new Guid("6cff4377-a1f8-4bf8-86df-83cfaf233410") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("69046331-69d0-4836-9a1d-b01eee942ecb"), new Guid("f92a1ffd-6630-437a-9d41-3763fcb80fdd") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("6da88131-a6ca-4ad0-be9a-95a6e2d137f4"), new Guid("241ccdad-4fdf-499b-b834-5057b2174599") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("6da88131-a6ca-4ad0-be9a-95a6e2d137f4"), new Guid("ec4e2fac-0ffa-4713-afcd-53af5b441138") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("746a0772-91b6-4283-90ec-bc6a8279b84f"), new Guid("adc23850-b45e-413f-b2d1-a7aabca606cb") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("746a0772-91b6-4283-90ec-bc6a8279b84f"), new Guid("ec4e2fac-0ffa-4713-afcd-53af5b441138") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("7d1bff9a-d86f-4656-a5b6-a19141b97554"), new Guid("241ccdad-4fdf-499b-b834-5057b2174599") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("7d1bff9a-d86f-4656-a5b6-a19141b97554"), new Guid("6cff4377-a1f8-4bf8-86df-83cfaf233410") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("80f9e2ef-8259-4515-98f7-51e852d4eece"), new Guid("7c717a3e-ccca-4e56-b4a5-36c83331deb4") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("80f9e2ef-8259-4515-98f7-51e852d4eece"), new Guid("94ca64b7-b8f5-474f-92b1-eede5e5d30d4") });

            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("2b9d3d39-9e2b-4103-9ec5-8dc258700ef3"), new Guid("a29750c7-a779-4b14-b693-24584cffb85a") });

            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("4038e59b-f787-45d6-9cdb-93ccf559e962"), new Guid("c39f2238-1b72-433f-8d33-f064fee7746d") });

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("0fb15901-2556-4ad8-896a-5a83b79c55f2"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("1560a201-38d3-4581-892e-bcf57047d1b4"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("3862dd30-1b4f-41f8-93e5-a33298e64ba4"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("3d10b203-858e-4e73-9e8a-6917d38fe2d5"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("67ecbac2-70c1-4fef-8b61-10232266d9a3"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("69046331-69d0-4836-9a1d-b01eee942ecb"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("6da88131-a6ca-4ad0-be9a-95a6e2d137f4"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("746a0772-91b6-4283-90ec-bc6a8279b84f"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("7d1bff9a-d86f-4656-a5b6-a19141b97554"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("80f9e2ef-8259-4515-98f7-51e852d4eece"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("241ccdad-4fdf-499b-b834-5057b2174599"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("3b5dc36c-258c-446d-98db-d4a733ac637b"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("45073a7c-2949-456e-97df-85f6109d7c88"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("6cff4377-a1f8-4bf8-86df-83cfaf233410"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("7c717a3e-ccca-4e56-b4a5-36c83331deb4"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("94ca64b7-b8f5-474f-92b1-eede5e5d30d4"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("99b05525-4d4b-435e-85b2-b4a3ff8d46fb"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("adc23850-b45e-413f-b2d1-a7aabca606cb"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("ec4e2fac-0ffa-4713-afcd-53af5b441138"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("f92a1ffd-6630-437a-9d41-3763fcb80fdd"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("2b9d3d39-9e2b-4103-9ec5-8dc258700ef3"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("4038e59b-f787-45d6-9cdb-93ccf559e962"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("0abbcbac-abd5-4ee0-9724-d6ab636c7afe"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("1322f4b2-8ad7-4714-9b83-262d26c1aba7"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("2e17a0c6-4cf8-4365-81d4-545f0e498e2d"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("814a5e44-95d3-46af-ad95-151d4011b8f9"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("84e5dcf5-acdf-4118-b2d6-c35a61fc5818"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("8698a42e-f4b6-40e0-8482-7a5ffd70b72c"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("89e5e30b-eea0-4f12-8bd7-14ab16be4a73"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("91102731-4c95-4d7a-8a59-c0c815fc6a3e"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("cfe2e4f7-b974-4ef8-af3d-2bf3b0e86169"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("e91f2955-8418-416a-a467-ec9f0e3e36ac"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a29750c7-a779-4b14-b693-24584cffb85a"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("c39f2238-1b72-433f-8d33-f064fee7746d"));

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("043e189d-3426-4a0a-99a1-2f610d97775c"), "Будівництво" },
                    { new Guid("06606f96-a585-4a23-aefe-8569dec6a6b1"), "Комунальні послуги" },
                    { new Guid("1d3a4c4b-0ed4-4aee-8c21-5abb766a8768"), "Сміття та екологія" },
                    { new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"), "Благоустрій" },
                    { new Guid("45b574f9-18a1-4aa1-bba6-9decb1f422ee"), "Транспорт" },
                    { new Guid("56b3d331-0738-4334-ab73-8096f3942d26"), "Парки та зелені зони" },
                    { new Guid("8dccf214-d951-43a6-ba7e-0df8a39b83bc"), "Інше" },
                    { new Guid("b30a439f-ed96-4df2-b741-620b64c36d05"), "Освітлення" },
                    { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), "Безпека" },
                    { new Guid("e5be1bd7-1a3e-4743-b8dc-d006e346893c"), "Дороги та тротуари" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("7699cd79-edcd-47c1-a114-8fe2d2fb3ed4"), "User" },
                    { new Guid("9d9e2996-daa9-4861-93c1-89a0d1449b25"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("7938af4f-4ab0-4dba-9923-3366bb4bc1a5"), "Виконано" },
                    { new Guid("7a0fe21a-2487-49e8-b603-36c373f5cdef"), "Потребує уточнення" },
                    { new Guid("8c986073-d30e-4815-901f-41aca95e9f52"), "Нова" },
                    { new Guid("a7829f55-3c01-4bd3-b835-7968a7dd2ca1"), "В роботі" },
                    { new Guid("de701d25-f9f1-40f8-8688-304a3f2314e3"), "Відхилено" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password_hash" },
                values: new object[] { new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da"), "admin@ostroh.edu.ua", "Адміністратор Острога", "o8mkEHImpZXmHZe23lYlCA==:YRn3mzOiCTP5Fi1zOoY6LfABVvSgHR1395aJQ0zGvE0=" });

            migrationBuilder.InsertData(
                table: "fk_user_roles",
                columns: new[] { "roles_id", "users_id" },
                values: new object[] { new Guid("9d9e2996-daa9-4861-93c1-89a0d1449b25"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") });

            migrationBuilder.InsertData(
                table: "problems",
                columns: new[] { "id", "created_at", "description", "latitude", "longitude", "status_id", "title", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("01c0b508-0f00-4dc6-9e3d-9c3d877e50b1"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4186), "Вже тиждень не світять ліхтарі на ділянці від будинку №10 до №20.", 50.328499999999998, 26.512499999999999, new Guid("a7829f55-3c01-4bd3-b835-7968a7dd2ca1"), "Не працює вуличне освітлення на вул. Семінарська", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4186), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("1f5b111b-b022-4088-a8a3-b227387ccf8e"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4176), "Велика яма на дорозі біля будинку №15. Потрібує термінового ремонту.", 50.3294, 26.514399999999998, new Guid("8c986073-d30e-4815-901f-41aca95e9f52"), "Розбита дорога на вул. Академічна", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4177), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("2480589f-c769-4d03-8540-b2e42d74b86d"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4193), "Старе дерево нахилилося і може впасти на дорогу. Небезпечно для пішоходів та транспорту.", 50.331200000000003, 26.509799999999998, new Guid("a7829f55-3c01-4bd3-b835-7968a7dd2ca1"), "Аварійне дерево на вул. Луцька", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4193), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("30397e1d-a5ed-416a-8b19-297f03651510"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4245), "Після дощу утворюються великі калюжі через відсутність каналізації.", 50.3279, 26.513200000000001, new Guid("8c986073-d30e-4815-901f-41aca95e9f52"), "Відсутня каналізація на вул. Садова", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4245), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("4ea45687-bc8d-4884-a69e-316373af2f3d"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4189), "Сміттєві контейнери не вивозяться вже 3 дні, сміття розкидане навколо.", 50.330100000000002, 26.5167, new Guid("8c986073-d30e-4815-901f-41aca95e9f52"), "Переповнені сміттєві баки біля ринку", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4189), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("6ccf7a13-628a-4d7f-b90a-307ee50ec804"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4232), "Світлофор на перехресті не працює вже другий день.", 50.331499999999998, 26.510200000000001, new Guid("a7829f55-3c01-4bd3-b835-7968a7dd2ca1"), "Не працює світлофор на вул. Луцька", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4232), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("6d82a36c-61db-4422-b27f-d30c3cb310d9"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4237), "Огорожа школи №1 іржава та потребує фарбування.", 50.328800000000001, 26.5138, new Guid("7938af4f-4ab0-4dba-9923-3366bb4bc1a5"), "Потребує фарбування огорожа школи", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4237), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("79866f66-49b7-4b59-8c1b-fd87d8545d44"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4220), "Великі тріщини на тротуарі, небезпечно для пішоходів, особливо в темний час доби.", 50.330500000000001, 26.513400000000001, new Guid("8c986073-d30e-4815-901f-41aca95e9f52"), "Тріщина на тротуарі вул. Папаніна", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4220), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("79ee6684-c6de-4ffc-b4c3-c9ddfc157e35"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4228), "Витік води з водопровідної труби, вода заливає дорогу.", 50.329099999999997, 26.517099999999999, new Guid("a7829f55-3c01-4bd3-b835-7968a7dd2ca1"), "Прорив водопроводу на вул. Замкова", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4228), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("7d1a6efc-d8fb-4943-aab5-091316e9ed6c"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4222), "Ведеться будівництво без відповідних дозволів, порушується архітектурний вигляд міста.", 50.329799999999999, 26.517800000000001, new Guid("de701d25-f9f1-40f8-8688-304a3f2314e3"), "Незаконне будівництво на вул. Князів Острозьких", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4222), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("7dcd8cc2-2b0d-46bd-ba58-4f623af4fe0b"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4230), "Клумби біля центральної площі не доглядаються, заросли бур'яном.", 50.329599999999999, 26.514900000000001, new Guid("8c986073-d30e-4815-901f-41aca95e9f52"), "Зарослі бур'яном клумби в центрі", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4230), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("7de73a55-98aa-44ba-a9c7-b8b4435ece3b"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4191), "Дерев'яна лавка зламана, потребує заміни або ремонту.", 50.327800000000003, 26.518899999999999, new Guid("7938af4f-4ab0-4dba-9923-3366bb4bc1a5"), "Зламана лавка в парку Тараса Шевченка", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4191), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("8f0ba736-4924-4bc7-a7dd-981597416aa6"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4243), "Гілки дерева загрожують електричним проводам.", 50.328600000000002, 26.514700000000001, new Guid("a7829f55-3c01-4bd3-b835-7968a7dd2ca1"), "Потребує обрізки дерева біля будинку", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4243), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("a089b92c-b40d-4b2d-afd3-b76c4c968a4f"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4247), "Вандали розмалювали фасад історичної будівлі в центрі міста.", 50.329300000000003, 26.517499999999998, new Guid("7938af4f-4ab0-4dba-9923-3366bb4bc1a5"), "Граффіті на фасаді історичної будівлі", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4247), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("a7a0ba1f-fea6-48f8-a01d-93a081aa3766"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4239), "На автобусній зупинці 'Центр' немає урн для сміття.", 50.330199999999998, 26.5153, new Guid("8c986073-d30e-4815-901f-41aca95e9f52"), "Відсутні урни на зупинці", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4239), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("acd45599-d4f1-46bf-b5dc-71e98448e91a"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4241), "Бордюр зруйнований на протяжності 5 метрів.", 50.329900000000002, 26.516400000000001, new Guid("8c986073-d30e-4815-901f-41aca95e9f52"), "Зламаний бордюр на вул. Князів Острозьких", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4241), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("b7ed6f34-7f79-42a2-b64c-cee98fe26986"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4197), "Пішохідний перехід біля школи №2 без розмітки, небезпечно для дітей.", 50.328899999999997, 26.515599999999999, new Guid("8c986073-d30e-4815-901f-41aca95e9f52"), "Відсутня розмітка на пішохідному переході", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4197), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("d4d698a9-3046-4c82-aa0f-19950b0eba39"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4224), "Гойдалки та гірка на дитячому майданчику в аварійному стані.", 50.328200000000002, 26.514199999999999, new Guid("a7829f55-3c01-4bd3-b835-7968a7dd2ca1"), "Потребує ремонту дитячий майданчик", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4224), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("effd7eae-cd41-459d-aad9-dfe9f7fea564"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4235), "Стихійне сміттєзвалище утворилося в лісосмузі за містом.", 50.327199999999998, 26.519500000000001, new Guid("8c986073-d30e-4815-901f-41aca95e9f52"), "Сміттєзвалище в лісосмузі", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4235), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("f90c71a8-2a06-4e7e-bb43-6c3f86eb7905"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4226), "На перехресті вул. Академічна та вул. Семінарська відсутній знак пріоритету.", 50.3307, 26.516100000000002, new Guid("8c986073-d30e-4815-901f-41aca95e9f52"), "Відсутній дорожній знак на перехресті", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4226), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") }
                });

            migrationBuilder.InsertData(
                table: "comments",
                columns: new[] { "id", "content", "created_at", "problem_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("0be906ea-f2ed-4766-b3db-de58dd9d3e34"), "Дякую, що виправили!", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4529), new Guid("7de73a55-98aa-44ba-a9c7-b8b4435ece3b"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("2c6c958a-24fa-4aa8-9c59-383e9b6dada8"), "Дійсно небезпечно, особливо при сильному вітрі.", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4531), new Guid("2480589f-c769-4d03-8540-b2e42d74b86d"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("2d950d76-8a5a-42f4-98d1-fdbabb2222dd"), "Так, підтверджую. Їздив сьогодні, ледь не зламав підвіску.", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4517), new Guid("1f5b111b-b022-4088-a8a3-b227387ccf8e"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("2e6513bf-6b33-45f1-a60f-194f21924ef1"), "Вода вже затопила половину дороги!", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4534), new Guid("79ee6684-c6de-4ffc-b4c3-c9ddfc157e35"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("4c4711ba-7263-4440-9b79-ccdde04da769"), "Це екологічна катастрофа для нашого міста!", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4537), new Guid("effd7eae-cd41-459d-aad9-dfe9f7fea564"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("4edb3f19-c8cf-4566-b345-e54d4ed1f4a7"), "Діти щодня переходять дорогу, це дуже небезпечно!", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4532), new Guid("b7ed6f34-7f79-42a2-b64c-cee98fe26986"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("69f3dc28-27f0-43cf-9a6f-60fb9f068c07"), "Дуже небезпечно ходити ввечері, треба терміново виправити.", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4527), new Guid("01c0b508-0f00-4dc6-9e3d-9c3d877e50b1"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("6f855aa1-3608-42b8-9c9a-64180da215da"), "Вже місяць як така ситуація. Коли вже відремонтують?", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4525), new Guid("1f5b111b-b022-4088-a8a3-b227387ccf8e"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("9ed70338-1112-44ec-bf79-b8a296d408f8"), "Жахлива ситуація, смердить на всю вулицю.", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4528), new Guid("4ea45687-bc8d-4884-a69e-316373af2f3d"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("9ff3f149-f1c4-4376-816c-a30dbfa8ca7b"), "Коли почнуть ремонт? Діти не можуть нормально гратися.", new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4533), new Guid("d4d698a9-3046-4c82-aa0f-19950b0eba39"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") }
                });

            migrationBuilder.InsertData(
                table: "fk_problem_categories",
                columns: new[] { "categories_id", "problems_id" },
                values: new object[,]
                {
                    { new Guid("043e189d-3426-4a0a-99a1-2f610d97775c"), new Guid("7d1a6efc-d8fb-4943-aab5-091316e9ed6c") },
                    { new Guid("06606f96-a585-4a23-aefe-8569dec6a6b1"), new Guid("30397e1d-a5ed-416a-8b19-297f03651510") },
                    { new Guid("06606f96-a585-4a23-aefe-8569dec6a6b1"), new Guid("79ee6684-c6de-4ffc-b4c3-c9ddfc157e35") },
                    { new Guid("1d3a4c4b-0ed4-4aee-8c21-5abb766a8768"), new Guid("4ea45687-bc8d-4884-a69e-316373af2f3d") },
                    { new Guid("1d3a4c4b-0ed4-4aee-8c21-5abb766a8768"), new Guid("a7a0ba1f-fea6-48f8-a01d-93a081aa3766") },
                    { new Guid("1d3a4c4b-0ed4-4aee-8c21-5abb766a8768"), new Guid("effd7eae-cd41-459d-aad9-dfe9f7fea564") },
                    { new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"), new Guid("6d82a36c-61db-4422-b27f-d30c3cb310d9") },
                    { new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"), new Guid("7dcd8cc2-2b0d-46bd-ba58-4f623af4fe0b") },
                    { new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"), new Guid("7de73a55-98aa-44ba-a9c7-b8b4435ece3b") },
                    { new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"), new Guid("a089b92c-b40d-4b2d-afd3-b76c4c968a4f") },
                    { new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"), new Guid("d4d698a9-3046-4c82-aa0f-19950b0eba39") },
                    { new Guid("45b574f9-18a1-4aa1-bba6-9decb1f422ee"), new Guid("6ccf7a13-628a-4d7f-b90a-307ee50ec804") },
                    { new Guid("45b574f9-18a1-4aa1-bba6-9decb1f422ee"), new Guid("a7a0ba1f-fea6-48f8-a01d-93a081aa3766") },
                    { new Guid("45b574f9-18a1-4aa1-bba6-9decb1f422ee"), new Guid("f90c71a8-2a06-4e7e-bb43-6c3f86eb7905") },
                    { new Guid("56b3d331-0738-4334-ab73-8096f3942d26"), new Guid("2480589f-c769-4d03-8540-b2e42d74b86d") },
                    { new Guid("56b3d331-0738-4334-ab73-8096f3942d26"), new Guid("7dcd8cc2-2b0d-46bd-ba58-4f623af4fe0b") },
                    { new Guid("56b3d331-0738-4334-ab73-8096f3942d26"), new Guid("7de73a55-98aa-44ba-a9c7-b8b4435ece3b") },
                    { new Guid("56b3d331-0738-4334-ab73-8096f3942d26"), new Guid("8f0ba736-4924-4bc7-a7dd-981597416aa6") },
                    { new Guid("b30a439f-ed96-4df2-b741-620b64c36d05"), new Guid("01c0b508-0f00-4dc6-9e3d-9c3d877e50b1") },
                    { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("2480589f-c769-4d03-8540-b2e42d74b86d") },
                    { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("6ccf7a13-628a-4d7f-b90a-307ee50ec804") },
                    { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("8f0ba736-4924-4bc7-a7dd-981597416aa6") },
                    { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("a089b92c-b40d-4b2d-afd3-b76c4c968a4f") },
                    { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("b7ed6f34-7f79-42a2-b64c-cee98fe26986") },
                    { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("d4d698a9-3046-4c82-aa0f-19950b0eba39") },
                    { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("f90c71a8-2a06-4e7e-bb43-6c3f86eb7905") },
                    { new Guid("e5be1bd7-1a3e-4743-b8dc-d006e346893c"), new Guid("1f5b111b-b022-4088-a8a3-b227387ccf8e") },
                    { new Guid("e5be1bd7-1a3e-4743-b8dc-d006e346893c"), new Guid("79866f66-49b7-4b59-8c1b-fd87d8545d44") },
                    { new Guid("e5be1bd7-1a3e-4743-b8dc-d006e346893c"), new Guid("acd45599-d4f1-46bf-b5dc-71e98448e91a") },
                    { new Guid("e5be1bd7-1a3e-4743-b8dc-d006e346893c"), new Guid("b7ed6f34-7f79-42a2-b64c-cee98fe26986") }
                });

            migrationBuilder.InsertData(
                table: "ratings",
                columns: new[] { "id", "created_at", "points", "problem_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("0107dfd0-ecc1-4246-8f72-280374fcb460"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4581), 5.0, new Guid("2480589f-c769-4d03-8540-b2e42d74b86d"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("227f26bc-0bc2-4902-b4c8-535e85774868"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4579), 5.0, new Guid("01c0b508-0f00-4dc6-9e3d-9c3d877e50b1"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("294f3d16-343f-49e5-a4ad-e502c6264681"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4580), 4.7999999999999998, new Guid("4ea45687-bc8d-4884-a69e-316373af2f3d"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("879f0347-50bf-4c06-8d8e-f8709f63ecbe"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4582), 4.9000000000000004, new Guid("b7ed6f34-7f79-42a2-b64c-cee98fe26986"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("abcd7788-694f-4a78-89a9-c2867e7cd4f7"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4574), 4.5, new Guid("1f5b111b-b022-4088-a8a3-b227387ccf8e"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("b04d7405-a007-4fe2-b032-63cc491c857c"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4590), 4.2999999999999998, new Guid("acd45599-d4f1-46bf-b5dc-71e98448e91a"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("cd9f4d27-d406-41d8-8af8-cd537ae7da07"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4588), 5.0, new Guid("79ee6684-c6de-4ffc-b4c3-c9ddfc157e35"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("d6335847-2415-40cd-9a49-4f00d7dd6759"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4584), 4.2000000000000002, new Guid("79866f66-49b7-4b59-8c1b-fd87d8545d44"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("e10cc8db-7cf3-4c8f-8eb2-eb31a1393f63"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4589), 4.5999999999999996, new Guid("effd7eae-cd41-459d-aad9-dfe9f7fea564"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") },
                    { new Guid("fef06e7a-248d-445f-928e-fa87367b7c90"), new DateTime(2025, 10, 20, 20, 47, 54, 654, DateTimeKind.Utc).AddTicks(4585), 4.7000000000000002, new Guid("f90c71a8-2a06-4e7e-bb43-6c3f86eb7905"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("8dccf214-d951-43a6-ba7e-0df8a39b83bc"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("0be906ea-f2ed-4766-b3db-de58dd9d3e34"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("2c6c958a-24fa-4aa8-9c59-383e9b6dada8"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("2d950d76-8a5a-42f4-98d1-fdbabb2222dd"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("2e6513bf-6b33-45f1-a60f-194f21924ef1"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("4c4711ba-7263-4440-9b79-ccdde04da769"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("4edb3f19-c8cf-4566-b345-e54d4ed1f4a7"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("69f3dc28-27f0-43cf-9a6f-60fb9f068c07"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("6f855aa1-3608-42b8-9c9a-64180da215da"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("9ed70338-1112-44ec-bf79-b8a296d408f8"));

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: new Guid("9ff3f149-f1c4-4376-816c-a30dbfa8ca7b"));

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("043e189d-3426-4a0a-99a1-2f610d97775c"), new Guid("7d1a6efc-d8fb-4943-aab5-091316e9ed6c") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("06606f96-a585-4a23-aefe-8569dec6a6b1"), new Guid("30397e1d-a5ed-416a-8b19-297f03651510") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("06606f96-a585-4a23-aefe-8569dec6a6b1"), new Guid("79ee6684-c6de-4ffc-b4c3-c9ddfc157e35") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("1d3a4c4b-0ed4-4aee-8c21-5abb766a8768"), new Guid("4ea45687-bc8d-4884-a69e-316373af2f3d") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("1d3a4c4b-0ed4-4aee-8c21-5abb766a8768"), new Guid("a7a0ba1f-fea6-48f8-a01d-93a081aa3766") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("1d3a4c4b-0ed4-4aee-8c21-5abb766a8768"), new Guid("effd7eae-cd41-459d-aad9-dfe9f7fea564") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"), new Guid("6d82a36c-61db-4422-b27f-d30c3cb310d9") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"), new Guid("7dcd8cc2-2b0d-46bd-ba58-4f623af4fe0b") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"), new Guid("7de73a55-98aa-44ba-a9c7-b8b4435ece3b") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"), new Guid("a089b92c-b40d-4b2d-afd3-b76c4c968a4f") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"), new Guid("d4d698a9-3046-4c82-aa0f-19950b0eba39") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("45b574f9-18a1-4aa1-bba6-9decb1f422ee"), new Guid("6ccf7a13-628a-4d7f-b90a-307ee50ec804") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("45b574f9-18a1-4aa1-bba6-9decb1f422ee"), new Guid("a7a0ba1f-fea6-48f8-a01d-93a081aa3766") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("45b574f9-18a1-4aa1-bba6-9decb1f422ee"), new Guid("f90c71a8-2a06-4e7e-bb43-6c3f86eb7905") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("56b3d331-0738-4334-ab73-8096f3942d26"), new Guid("2480589f-c769-4d03-8540-b2e42d74b86d") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("56b3d331-0738-4334-ab73-8096f3942d26"), new Guid("7dcd8cc2-2b0d-46bd-ba58-4f623af4fe0b") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("56b3d331-0738-4334-ab73-8096f3942d26"), new Guid("7de73a55-98aa-44ba-a9c7-b8b4435ece3b") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("56b3d331-0738-4334-ab73-8096f3942d26"), new Guid("8f0ba736-4924-4bc7-a7dd-981597416aa6") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("b30a439f-ed96-4df2-b741-620b64c36d05"), new Guid("01c0b508-0f00-4dc6-9e3d-9c3d877e50b1") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("2480589f-c769-4d03-8540-b2e42d74b86d") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("6ccf7a13-628a-4d7f-b90a-307ee50ec804") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("8f0ba736-4924-4bc7-a7dd-981597416aa6") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("a089b92c-b40d-4b2d-afd3-b76c4c968a4f") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("b7ed6f34-7f79-42a2-b64c-cee98fe26986") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("d4d698a9-3046-4c82-aa0f-19950b0eba39") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"), new Guid("f90c71a8-2a06-4e7e-bb43-6c3f86eb7905") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("e5be1bd7-1a3e-4743-b8dc-d006e346893c"), new Guid("1f5b111b-b022-4088-a8a3-b227387ccf8e") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("e5be1bd7-1a3e-4743-b8dc-d006e346893c"), new Guid("79866f66-49b7-4b59-8c1b-fd87d8545d44") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("e5be1bd7-1a3e-4743-b8dc-d006e346893c"), new Guid("acd45599-d4f1-46bf-b5dc-71e98448e91a") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("e5be1bd7-1a3e-4743-b8dc-d006e346893c"), new Guid("b7ed6f34-7f79-42a2-b64c-cee98fe26986") });

            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("9d9e2996-daa9-4861-93c1-89a0d1449b25"), new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da") });

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("0107dfd0-ecc1-4246-8f72-280374fcb460"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("227f26bc-0bc2-4902-b4c8-535e85774868"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("294f3d16-343f-49e5-a4ad-e502c6264681"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("879f0347-50bf-4c06-8d8e-f8709f63ecbe"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("abcd7788-694f-4a78-89a9-c2867e7cd4f7"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("b04d7405-a007-4fe2-b032-63cc491c857c"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("cd9f4d27-d406-41d8-8af8-cd537ae7da07"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("d6335847-2415-40cd-9a49-4f00d7dd6759"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("e10cc8db-7cf3-4c8f-8eb2-eb31a1393f63"));

            migrationBuilder.DeleteData(
                table: "ratings",
                keyColumn: "id",
                keyValue: new Guid("fef06e7a-248d-445f-928e-fa87367b7c90"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("7699cd79-edcd-47c1-a114-8fe2d2fb3ed4"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("7a0fe21a-2487-49e8-b603-36c373f5cdef"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("043e189d-3426-4a0a-99a1-2f610d97775c"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("06606f96-a585-4a23-aefe-8569dec6a6b1"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("1d3a4c4b-0ed4-4aee-8c21-5abb766a8768"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("2dade5b6-d7a4-4842-be11-72ab622f787c"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("45b574f9-18a1-4aa1-bba6-9decb1f422ee"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("56b3d331-0738-4334-ab73-8096f3942d26"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("b30a439f-ed96-4df2-b741-620b64c36d05"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("da3e40eb-2fd6-4e23-8bf0-ed84a642563d"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("e5be1bd7-1a3e-4743-b8dc-d006e346893c"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("01c0b508-0f00-4dc6-9e3d-9c3d877e50b1"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("1f5b111b-b022-4088-a8a3-b227387ccf8e"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("2480589f-c769-4d03-8540-b2e42d74b86d"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("30397e1d-a5ed-416a-8b19-297f03651510"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("4ea45687-bc8d-4884-a69e-316373af2f3d"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("6ccf7a13-628a-4d7f-b90a-307ee50ec804"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("6d82a36c-61db-4422-b27f-d30c3cb310d9"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("79866f66-49b7-4b59-8c1b-fd87d8545d44"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("79ee6684-c6de-4ffc-b4c3-c9ddfc157e35"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("7d1a6efc-d8fb-4943-aab5-091316e9ed6c"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("7dcd8cc2-2b0d-46bd-ba58-4f623af4fe0b"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("7de73a55-98aa-44ba-a9c7-b8b4435ece3b"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("8f0ba736-4924-4bc7-a7dd-981597416aa6"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("a089b92c-b40d-4b2d-afd3-b76c4c968a4f"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("a7a0ba1f-fea6-48f8-a01d-93a081aa3766"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("acd45599-d4f1-46bf-b5dc-71e98448e91a"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("b7ed6f34-7f79-42a2-b64c-cee98fe26986"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("d4d698a9-3046-4c82-aa0f-19950b0eba39"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("effd7eae-cd41-459d-aad9-dfe9f7fea564"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("f90c71a8-2a06-4e7e-bb43-6c3f86eb7905"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("9d9e2996-daa9-4861-93c1-89a0d1449b25"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("7938af4f-4ab0-4dba-9923-3366bb4bc1a5"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("8c986073-d30e-4815-901f-41aca95e9f52"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("a7829f55-3c01-4bd3-b835-7968a7dd2ca1"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("de701d25-f9f1-40f8-8688-304a3f2314e3"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("12e667a5-425a-4fc1-b852-7f73b0bfa3da"));

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("0fb15901-2556-4ad8-896a-5a83b79c55f2"), "Category 2" },
                    { new Guid("1560a201-38d3-4581-892e-bcf57047d1b4"), "Category 1" },
                    { new Guid("3862dd30-1b4f-41f8-93e5-a33298e64ba4"), "Category 8" },
                    { new Guid("3d10b203-858e-4e73-9e8a-6917d38fe2d5"), "Category 3" },
                    { new Guid("67ecbac2-70c1-4fef-8b61-10232266d9a3"), "Category 9" },
                    { new Guid("69046331-69d0-4836-9a1d-b01eee942ecb"), "Category 7" },
                    { new Guid("6da88131-a6ca-4ad0-be9a-95a6e2d137f4"), "Category 5" },
                    { new Guid("746a0772-91b6-4283-90ec-bc6a8279b84f"), "Category 4" },
                    { new Guid("7d1bff9a-d86f-4656-a5b6-a19141b97554"), "Category 6" },
                    { new Guid("80f9e2ef-8259-4515-98f7-51e852d4eece"), "Category 10" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("2b9d3d39-9e2b-4103-9ec5-8dc258700ef3"), "User" },
                    { new Guid("4038e59b-f787-45d6-9cdb-93ccf559e962"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("0abbcbac-abd5-4ee0-9724-d6ab636c7afe"), "Status 2" },
                    { new Guid("1322f4b2-8ad7-4714-9b83-262d26c1aba7"), "Status 6" },
                    { new Guid("2e17a0c6-4cf8-4365-81d4-545f0e498e2d"), "Status 10" },
                    { new Guid("814a5e44-95d3-46af-ad95-151d4011b8f9"), "Status 1" },
                    { new Guid("84e5dcf5-acdf-4118-b2d6-c35a61fc5818"), "Status 8" },
                    { new Guid("8698a42e-f4b6-40e0-8482-7a5ffd70b72c"), "Status 4" },
                    { new Guid("89e5e30b-eea0-4f12-8bd7-14ab16be4a73"), "Status 7" },
                    { new Guid("91102731-4c95-4d7a-8a59-c0c815fc6a3e"), "Status 9" },
                    { new Guid("cfe2e4f7-b974-4ef8-af3d-2bf3b0e86169"), "Status 3" },
                    { new Guid("e91f2955-8418-416a-a467-ec9f0e3e36ac"), "Status 5" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password_hash" },
                values: new object[,]
                {
                    { new Guid("a29750c7-a779-4b14-b693-24584cffb85a"), "user@example.com", "user", "7JUnoEIB4OCIg9sw6YIVhA==:FiwL3Qqt5RyBHEoUONt58fqhINuhHCQEwUKYpuaQoFQ=" },
                    { new Guid("c39f2238-1b72-433f-8d33-f064fee7746d"), "admin@example.com", "admin", "O1+fYlA6odci1pAjpTeH/g==:TJ1zQ1M//MBLEi2yGWSLkUkL/VP9UgnYLec6ggfK23o=" }
                });

            migrationBuilder.InsertData(
                table: "fk_user_roles",
                columns: new[] { "roles_id", "users_id" },
                values: new object[,]
                {
                    { new Guid("2b9d3d39-9e2b-4103-9ec5-8dc258700ef3"), new Guid("a29750c7-a779-4b14-b693-24584cffb85a") },
                    { new Guid("4038e59b-f787-45d6-9cdb-93ccf559e962"), new Guid("c39f2238-1b72-433f-8d33-f064fee7746d") }
                });

            migrationBuilder.InsertData(
                table: "problems",
                columns: new[] { "id", "created_at", "description", "latitude", "longitude", "status_id", "title", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("241ccdad-4fdf-499b-b834-5057b2174599"), new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5576), "Description of problem 5", 50.5, 30.5, new Guid("1322f4b2-8ad7-4714-9b83-262d26c1aba7"), "Problem 5", new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5576), new Guid("a29750c7-a779-4b14-b693-24584cffb85a") },
                    { new Guid("3b5dc36c-258c-446d-98db-d4a733ac637b"), new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5511), "Description of problem 1", 50.100000000000001, 30.100000000000001, new Guid("0abbcbac-abd5-4ee0-9724-d6ab636c7afe"), "Problem 1", new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5512), new Guid("a29750c7-a779-4b14-b693-24584cffb85a") },
                    { new Guid("45073a7c-2949-456e-97df-85f6109d7c88"), new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5586), "Description of problem 8", 50.799999999999997, 30.800000000000001, new Guid("91102731-4c95-4d7a-8a59-c0c815fc6a3e"), "Problem 8", new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5586), new Guid("c39f2238-1b72-433f-8d33-f064fee7746d") },
                    { new Guid("6cff4377-a1f8-4bf8-86df-83cfaf233410"), new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5579), "Description of problem 6", 50.600000000000001, 30.600000000000001, new Guid("89e5e30b-eea0-4f12-8bd7-14ab16be4a73"), "Problem 6", new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5580), new Guid("c39f2238-1b72-433f-8d33-f064fee7746d") },
                    { new Guid("7c717a3e-ccca-4e56-b4a5-36c83331deb4"), new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5592), "Description of problem 10", 51.0, 31.0, new Guid("814a5e44-95d3-46af-ad95-151d4011b8f9"), "Problem 10", new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5592), new Guid("c39f2238-1b72-433f-8d33-f064fee7746d") },
                    { new Guid("94ca64b7-b8f5-474f-92b1-eede5e5d30d4"), new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5588), "Description of problem 9", 50.899999999999999, 30.899999999999999, new Guid("2e17a0c6-4cf8-4365-81d4-545f0e498e2d"), "Problem 9", new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5589), new Guid("a29750c7-a779-4b14-b693-24584cffb85a") },
                    { new Guid("99b05525-4d4b-435e-85b2-b4a3ff8d46fb"), new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5565), "Description of problem 2", 50.200000000000003, 30.199999999999999, new Guid("cfe2e4f7-b974-4ef8-af3d-2bf3b0e86169"), "Problem 2", new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5565), new Guid("c39f2238-1b72-433f-8d33-f064fee7746d") },
                    { new Guid("adc23850-b45e-413f-b2d1-a7aabca606cb"), new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5568), "Description of problem 3", 50.299999999999997, 30.300000000000001, new Guid("8698a42e-f4b6-40e0-8482-7a5ffd70b72c"), "Problem 3", new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5568), new Guid("a29750c7-a779-4b14-b693-24584cffb85a") },
                    { new Guid("ec4e2fac-0ffa-4713-afcd-53af5b441138"), new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5571), "Description of problem 4", 50.399999999999999, 30.399999999999999, new Guid("e91f2955-8418-416a-a467-ec9f0e3e36ac"), "Problem 4", new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5572), new Guid("c39f2238-1b72-433f-8d33-f064fee7746d") },
                    { new Guid("f92a1ffd-6630-437a-9d41-3763fcb80fdd"), new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5582), "Description of problem 7", 50.700000000000003, 30.699999999999999, new Guid("84e5dcf5-acdf-4118-b2d6-c35a61fc5818"), "Problem 7", new DateTime(2025, 10, 18, 19, 25, 58, 571, DateTimeKind.Utc).AddTicks(5583), new Guid("a29750c7-a779-4b14-b693-24584cffb85a") }
                });

            migrationBuilder.InsertData(
                table: "fk_problem_categories",
                columns: new[] { "categories_id", "problems_id" },
                values: new object[,]
                {
                    { new Guid("0fb15901-2556-4ad8-896a-5a83b79c55f2"), new Guid("3b5dc36c-258c-446d-98db-d4a733ac637b") },
                    { new Guid("0fb15901-2556-4ad8-896a-5a83b79c55f2"), new Guid("99b05525-4d4b-435e-85b2-b4a3ff8d46fb") },
                    { new Guid("1560a201-38d3-4581-892e-bcf57047d1b4"), new Guid("3b5dc36c-258c-446d-98db-d4a733ac637b") },
                    { new Guid("1560a201-38d3-4581-892e-bcf57047d1b4"), new Guid("7c717a3e-ccca-4e56-b4a5-36c83331deb4") },
                    { new Guid("3862dd30-1b4f-41f8-93e5-a33298e64ba4"), new Guid("45073a7c-2949-456e-97df-85f6109d7c88") },
                    { new Guid("3862dd30-1b4f-41f8-93e5-a33298e64ba4"), new Guid("f92a1ffd-6630-437a-9d41-3763fcb80fdd") },
                    { new Guid("3d10b203-858e-4e73-9e8a-6917d38fe2d5"), new Guid("99b05525-4d4b-435e-85b2-b4a3ff8d46fb") },
                    { new Guid("3d10b203-858e-4e73-9e8a-6917d38fe2d5"), new Guid("adc23850-b45e-413f-b2d1-a7aabca606cb") },
                    { new Guid("67ecbac2-70c1-4fef-8b61-10232266d9a3"), new Guid("45073a7c-2949-456e-97df-85f6109d7c88") },
                    { new Guid("67ecbac2-70c1-4fef-8b61-10232266d9a3"), new Guid("94ca64b7-b8f5-474f-92b1-eede5e5d30d4") },
                    { new Guid("69046331-69d0-4836-9a1d-b01eee942ecb"), new Guid("6cff4377-a1f8-4bf8-86df-83cfaf233410") },
                    { new Guid("69046331-69d0-4836-9a1d-b01eee942ecb"), new Guid("f92a1ffd-6630-437a-9d41-3763fcb80fdd") },
                    { new Guid("6da88131-a6ca-4ad0-be9a-95a6e2d137f4"), new Guid("241ccdad-4fdf-499b-b834-5057b2174599") },
                    { new Guid("6da88131-a6ca-4ad0-be9a-95a6e2d137f4"), new Guid("ec4e2fac-0ffa-4713-afcd-53af5b441138") },
                    { new Guid("746a0772-91b6-4283-90ec-bc6a8279b84f"), new Guid("adc23850-b45e-413f-b2d1-a7aabca606cb") },
                    { new Guid("746a0772-91b6-4283-90ec-bc6a8279b84f"), new Guid("ec4e2fac-0ffa-4713-afcd-53af5b441138") },
                    { new Guid("7d1bff9a-d86f-4656-a5b6-a19141b97554"), new Guid("241ccdad-4fdf-499b-b834-5057b2174599") },
                    { new Guid("7d1bff9a-d86f-4656-a5b6-a19141b97554"), new Guid("6cff4377-a1f8-4bf8-86df-83cfaf233410") },
                    { new Guid("80f9e2ef-8259-4515-98f7-51e852d4eece"), new Guid("7c717a3e-ccca-4e56-b4a5-36c83331deb4") },
                    { new Guid("80f9e2ef-8259-4515-98f7-51e852d4eece"), new Guid("94ca64b7-b8f5-474f-92b1-eede5e5d30d4") }
                });
        }
    }
}

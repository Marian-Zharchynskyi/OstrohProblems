using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedCoordinatorColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "coordinator_comment",
                table: "problems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "coordinator_id",
                table: "problems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rejection_reason",
                table: "problems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user_confirmation_status",
                table: "problems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Administrator" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "User" }
                });

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

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password_hash" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000010"), "admin@ostroh.edu.ua", "Адміністратор Острога", "NDCKzXgX45UpNrhtRVN87Q==:lTbqjHEkq3X+W7Eix4idgBm+AJQPZRiONiytP7erDtU=" });

            migrationBuilder.InsertData(
                table: "fk_user_roles",
                columns: new[] { "roles_id", "users_id" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000010") });

            migrationBuilder.InsertData(
                table: "problems",
                columns: new[] { "id", "coordinator_comment", "coordinator_id", "created_at", "description", "latitude", "longitude", "rejection_reason", "status_id", "title", "updated_at", "user_confirmation_status", "user_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000301"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Велика яма на дорозі біля будинку №15. Потрібує термінового ремонту.", 50.3294, 26.514399999999998, null, new Guid("00000000-0000-0000-0000-000000000201"), "Розбита дорога на вул. Академічна", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000302"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Вже тиждень не світять ліхтарі на ділянці від будинку №10 до №20.", 50.328499999999998, 26.512499999999999, null, new Guid("00000000-0000-0000-0000-000000000202"), "Не працює вуличне освітлення на вул. Семінарська", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000303"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Сміттєві контейнери не вивозяться вже 3 дні, сміття розкидане навколо.", 50.330100000000002, 26.5167, null, new Guid("00000000-0000-0000-0000-000000000201"), "Переповнені сміттєві баки біля ринку", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000304"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Дерев'яна лавка зламана, потребує заміни або ремонту.", 50.327800000000003, 26.518899999999999, null, new Guid("00000000-0000-0000-0000-000000000203"), "Зламана лавка в парку Тараса Шевченка", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000305"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Старе дерево нахилилося і може впасти на дорогу. Небезпечно для пішоходів та транспорту.", 50.331200000000003, 26.509799999999998, null, new Guid("00000000-0000-0000-0000-000000000202"), "Аварійне дерево на вул. Луцька", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000306"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Пішохідний перехід біля школи №2 без розмітки, небезпечно для дітей.", 50.328899999999997, 26.515599999999999, null, new Guid("00000000-0000-0000-0000-000000000201"), "Відсутня розмітка на пішохідному переході", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000307"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Великі тріщини на тротуарі, небезпечно для пішоходів, особливо в темний час доби.", 50.330500000000001, 26.513400000000001, null, new Guid("00000000-0000-0000-0000-000000000201"), "Тріщина на тротуарі вул. Папаніна", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000308"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ведеться будівництво без відповідних дозволів, порушується архітектурний вигляд міста.", 50.329799999999999, 26.517800000000001, null, new Guid("00000000-0000-0000-0000-000000000204"), "Незаконне будівництво на вул. Князів Острозьких", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000309"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Гойдалки та гірка на дитячому майданчику в аварійному стані.", 50.328200000000002, 26.514199999999999, null, new Guid("00000000-0000-0000-0000-000000000202"), "Потребує ремонту дитячий майданчик", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-00000000030a"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "На перехресті вул. Академічна та вул. Семінарська відсутній знак пріоритету.", 50.3307, 26.516100000000002, null, new Guid("00000000-0000-0000-0000-000000000201"), "Відсутній дорожній знак на перехресті", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-00000000030b"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Витік води з водопровідної труби, вода заливає дорогу.", 50.329099999999997, 26.517099999999999, null, new Guid("00000000-0000-0000-0000-000000000202"), "Прорив водопроводу на вул. Замкова", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-00000000030c"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Клумби біля центральної площі не доглядаються, заросли бур'яном.", 50.329599999999999, 26.514900000000001, null, new Guid("00000000-0000-0000-0000-000000000201"), "Зарослі бур'яном клумби в центрі", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-00000000030d"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Світлофор на перехресті не працює вже другий день.", 50.331499999999998, 26.510200000000001, null, new Guid("00000000-0000-0000-0000-000000000202"), "Не працює світлофор на вул. Луцька", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-00000000030e"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Стихійне сміттєзвалище утворилося в лісосмузі за містом.", 50.327199999999998, 26.519500000000001, null, new Guid("00000000-0000-0000-0000-000000000201"), "Сміттєзвалище в лісосмузі", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-00000000030f"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Огорожа школи №1 іржава та потребує фарбування.", 50.328800000000001, 26.5138, null, new Guid("00000000-0000-0000-0000-000000000203"), "Потребує фарбування огорожа школи", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000310"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "На автобусній зупинці 'Центр' немає урн для сміття.", 50.330199999999998, 26.5153, null, new Guid("00000000-0000-0000-0000-000000000201"), "Відсутні урни на зупинці", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000311"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Бордюр зруйнований на протяжності 5 метрів.", 50.329900000000002, 26.516400000000001, null, new Guid("00000000-0000-0000-0000-000000000201"), "Зламаний бордюр на вул. Князів Острозьких", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000312"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Гілки дерева загрожують електричним проводам.", 50.328600000000002, 26.514700000000001, null, new Guid("00000000-0000-0000-0000-000000000202"), "Потребує обрізки дерева біля будинку", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000313"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Після дощу утворюються великі калюжі через відсутність каналізації.", 50.3279, 26.513200000000001, null, new Guid("00000000-0000-0000-0000-000000000201"), "Відсутня каналізація на вул. Садова", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000314"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Вандали розмалювали фасад історичної будівлі в центрі міста.", 50.329300000000003, 26.517499999999998, null, new Guid("00000000-0000-0000-0000-000000000203"), "Граффіті на фасаді історичної будівлі", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("00000000-0000-0000-0000-000000000010") }
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
                name: "ix_problems_coordinator_id",
                table: "problems",
                column: "coordinator_id");

            migrationBuilder.AddForeignKey(
                name: "fk_problems_users_coordinator_id",
                table: "problems",
                column: "coordinator_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_problems_users_coordinator_id",
                table: "problems");

            migrationBuilder.DropIndex(
                name: "ix_problems_coordinator_id",
                table: "problems");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000010a"));

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
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000101"), new Guid("00000000-0000-0000-0000-000000000301") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000101"), new Guid("00000000-0000-0000-0000-000000000306") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000101"), new Guid("00000000-0000-0000-0000-000000000307") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000101"), new Guid("00000000-0000-0000-0000-000000000311") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000102"), new Guid("00000000-0000-0000-0000-000000000302") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000103"), new Guid("00000000-0000-0000-0000-000000000304") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000103"), new Guid("00000000-0000-0000-0000-000000000309") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000103"), new Guid("00000000-0000-0000-0000-00000000030c") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000103"), new Guid("00000000-0000-0000-0000-00000000030f") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000103"), new Guid("00000000-0000-0000-0000-000000000314") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000104"), new Guid("00000000-0000-0000-0000-000000000303") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000104"), new Guid("00000000-0000-0000-0000-00000000030e") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000104"), new Guid("00000000-0000-0000-0000-000000000310") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000105"), new Guid("00000000-0000-0000-0000-00000000030b") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000105"), new Guid("00000000-0000-0000-0000-000000000313") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000106"), new Guid("00000000-0000-0000-0000-00000000030a") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000106"), new Guid("00000000-0000-0000-0000-00000000030d") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000106"), new Guid("00000000-0000-0000-0000-000000000310") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-000000000305") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-000000000306") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-000000000309") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-00000000030a") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-00000000030d") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-000000000312") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000107"), new Guid("00000000-0000-0000-0000-000000000314") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000108"), new Guid("00000000-0000-0000-0000-000000000304") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000108"), new Guid("00000000-0000-0000-0000-000000000305") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000108"), new Guid("00000000-0000-0000-0000-00000000030c") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000108"), new Guid("00000000-0000-0000-0000-000000000312") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000109"), new Guid("00000000-0000-0000-0000-000000000308") });

            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000010") });

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
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000205"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"));

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
                keyValue: new Guid("00000000-0000-0000-0000-000000000308"));

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
                keyValue: new Guid("00000000-0000-0000-0000-00000000030c"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030d"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000030e"));

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
                keyValue: new Guid("00000000-0000-0000-0000-000000000311"));

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
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000201"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000202"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000203"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000204"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DropColumn(
                name: "coordinator_comment",
                table: "problems");

            migrationBuilder.DropColumn(
                name: "coordinator_id",
                table: "problems");

            migrationBuilder.DropColumn(
                name: "rejection_reason",
                table: "problems");

            migrationBuilder.DropColumn(
                name: "user_confirmation_status",
                table: "problems");

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
    }
}

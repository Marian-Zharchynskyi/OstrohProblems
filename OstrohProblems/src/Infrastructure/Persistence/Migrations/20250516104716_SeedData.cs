using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("0e781b8d-f8e4-4162-8cd8-7a9684333913"), new Guid("9b3a70f6-2eaf-445d-ae45-3088561175cf") });

            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("bb36f988-a230-4e1f-b961-5949a1d72d47"), new Guid("fde07caa-1a06-4a14-8de8-330fce2426eb") });

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("0e781b8d-f8e4-4162-8cd8-7a9684333913"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("bb36f988-a230-4e1f-b961-5949a1d72d47"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("9b3a70f6-2eaf-445d-ae45-3088561175cf"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("fde07caa-1a06-4a14-8de8-330fce2426eb"));

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("002a5593-f9a5-4daf-92e8-1e6425306a06"), "Category 8" },
                    { new Guid("0b9ba76d-de9c-4095-b055-e1e5c39f809f"), "Category 4" },
                    { new Guid("13240a79-cd16-4c5b-b974-9ee10084d17f"), "Category 2" },
                    { new Guid("1c0dc3aa-f763-4c8b-83c0-32b5455655cb"), "Category 6" },
                    { new Guid("4f125acf-5f5f-4039-b515-cd35905a37de"), "Category 9" },
                    { new Guid("75d44fbc-d180-44f0-b07e-9226e6dceaf2"), "Category 3" },
                    { new Guid("7b2f5794-a072-4a44-ab22-5728ea03e41a"), "Category 7" },
                    { new Guid("c8b2a272-30b4-4668-b75c-6da882be7323"), "Category 10" },
                    { new Guid("e2e8a3bc-c44d-47ca-b088-35290b300760"), "Category 1" },
                    { new Guid("fe096551-1df3-43aa-b091-e3928a586594"), "Category 5" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("79b9ff35-cce2-4eda-b076-bb91c4de2a7e"), "User" },
                    { new Guid("ca597483-af5c-4a11-8a12-c7332898280e"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("04629bec-7011-4f18-81b4-cd07b3717c06"), "Status 4" },
                    { new Guid("13f24a9d-faae-4741-b2ab-feb26b7ea5b4"), "Status 2" },
                    { new Guid("39718f61-7d56-42f3-ba22-7f9b0a1951d0"), "Status 6" },
                    { new Guid("428b287b-6109-426b-a86e-0497077cf624"), "Status 7" },
                    { new Guid("9d11abda-838c-40ba-bff5-5f7545abf156"), "Status 8" },
                    { new Guid("a8b3664e-7e96-4076-84ea-eb616d298729"), "Status 10" },
                    { new Guid("af12052c-8e8c-48fe-91ed-d38016d5ea79"), "Status 3" },
                    { new Guid("c126319e-7de1-4905-9b46-e811c86253d9"), "Status 5" },
                    { new Guid("e0235e56-3308-42c9-98e9-ce5fb5c71d95"), "Status 9" },
                    { new Guid("e727cd2c-58a2-4837-b96b-e7f397af7811"), "Status 1" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password_hash" },
                values: new object[,]
                {
                    { new Guid("4c69ccd8-d987-488a-8605-6aad3a2163e9"), "admin@example.com", "admin", "feU1/OuQgIi3heuhXbwSTA==:quuNvvRHJ69diK66GrEUVjczcOdvfJ26jry/Z2RV1Mw=" },
                    { new Guid("f66d83de-d0cd-4a42-9fa4-1bf581016fe9"), "user@example.com", "user", "NP9XGt/W1wSMA4F2Q+6haQ==:D9WqqyValg403ifZKx/Egg+eOPSPSu55uRlLsJnJ2nk=" }
                });

            migrationBuilder.InsertData(
                table: "fk_user_roles",
                columns: new[] { "roles_id", "users_id" },
                values: new object[,]
                {
                    { new Guid("79b9ff35-cce2-4eda-b076-bb91c4de2a7e"), new Guid("f66d83de-d0cd-4a42-9fa4-1bf581016fe9") },
                    { new Guid("ca597483-af5c-4a11-8a12-c7332898280e"), new Guid("4c69ccd8-d987-488a-8605-6aad3a2163e9") }
                });

            migrationBuilder.InsertData(
                table: "problems",
                columns: new[] { "id", "created_at", "description", "latitude", "longitude", "status_id", "title", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("07179785-65fd-4a8c-bc6d-81e0220518f9"), new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(328), "Description of problem 5", 50.5, 30.5, new Guid("39718f61-7d56-42f3-ba22-7f9b0a1951d0"), "Problem 5", new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(328), new Guid("f66d83de-d0cd-4a42-9fa4-1bf581016fe9") },
                    { new Guid("08723ab2-f62a-4994-8c0d-9436c542dc0c"), new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(348), "Description of problem 10", 51.0, 31.0, new Guid("e727cd2c-58a2-4837-b96b-e7f397af7811"), "Problem 10", new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(348), new Guid("4c69ccd8-d987-488a-8605-6aad3a2163e9") },
                    { new Guid("10c50b37-6044-4dcc-992d-03ca646425db"), new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(332), "Description of problem 6", 50.600000000000001, 30.600000000000001, new Guid("428b287b-6109-426b-a86e-0497077cf624"), "Problem 6", new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(333), new Guid("4c69ccd8-d987-488a-8605-6aad3a2163e9") },
                    { new Guid("2752d934-80d4-49a3-80af-58a5a05205ac"), new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(336), "Description of problem 7", 50.700000000000003, 30.699999999999999, new Guid("9d11abda-838c-40ba-bff5-5f7545abf156"), "Problem 7", new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(336), new Guid("f66d83de-d0cd-4a42-9fa4-1bf581016fe9") },
                    { new Guid("2b6d15a1-5f21-4fd7-a9c7-883e2ab83f4f"), new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(315), "Description of problem 2", 50.200000000000003, 30.199999999999999, new Guid("af12052c-8e8c-48fe-91ed-d38016d5ea79"), "Problem 2", new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(315), new Guid("4c69ccd8-d987-488a-8605-6aad3a2163e9") },
                    { new Guid("504018b3-9628-4d74-a8ff-8bbac60553a7"), new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(300), "Description of problem 1", 50.100000000000001, 30.100000000000001, new Guid("13f24a9d-faae-4741-b2ab-feb26b7ea5b4"), "Problem 1", new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(304), new Guid("f66d83de-d0cd-4a42-9fa4-1bf581016fe9") },
                    { new Guid("86d3dede-fdb8-4c18-af89-5976b7408c4c"), new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(318), "Description of problem 3", 50.299999999999997, 30.300000000000001, new Guid("04629bec-7011-4f18-81b4-cd07b3717c06"), "Problem 3", new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(319), new Guid("f66d83de-d0cd-4a42-9fa4-1bf581016fe9") },
                    { new Guid("90b029af-6d3d-47d6-9271-af0abb925d0e"), new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(343), "Description of problem 9", 50.899999999999999, 30.899999999999999, new Guid("a8b3664e-7e96-4076-84ea-eb616d298729"), "Problem 9", new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(343), new Guid("f66d83de-d0cd-4a42-9fa4-1bf581016fe9") },
                    { new Guid("bad37d13-3906-49c4-b776-1e7e4b780955"), new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(322), "Description of problem 4", 50.399999999999999, 30.399999999999999, new Guid("c126319e-7de1-4905-9b46-e811c86253d9"), "Problem 4", new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(322), new Guid("4c69ccd8-d987-488a-8605-6aad3a2163e9") },
                    { new Guid("f185924c-ce14-45ae-820a-efcbaba77149"), new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(339), "Description of problem 8", 50.799999999999997, 30.800000000000001, new Guid("e0235e56-3308-42c9-98e9-ce5fb5c71d95"), "Problem 8", new DateTime(2025, 5, 16, 10, 47, 14, 544, DateTimeKind.Utc).AddTicks(340), new Guid("4c69ccd8-d987-488a-8605-6aad3a2163e9") }
                });

            migrationBuilder.InsertData(
                table: "fk_problem_categories",
                columns: new[] { "categories_id", "problems_id" },
                values: new object[,]
                {
                    { new Guid("002a5593-f9a5-4daf-92e8-1e6425306a06"), new Guid("2752d934-80d4-49a3-80af-58a5a05205ac") },
                    { new Guid("002a5593-f9a5-4daf-92e8-1e6425306a06"), new Guid("f185924c-ce14-45ae-820a-efcbaba77149") },
                    { new Guid("0b9ba76d-de9c-4095-b055-e1e5c39f809f"), new Guid("86d3dede-fdb8-4c18-af89-5976b7408c4c") },
                    { new Guid("0b9ba76d-de9c-4095-b055-e1e5c39f809f"), new Guid("bad37d13-3906-49c4-b776-1e7e4b780955") },
                    { new Guid("13240a79-cd16-4c5b-b974-9ee10084d17f"), new Guid("2b6d15a1-5f21-4fd7-a9c7-883e2ab83f4f") },
                    { new Guid("13240a79-cd16-4c5b-b974-9ee10084d17f"), new Guid("504018b3-9628-4d74-a8ff-8bbac60553a7") },
                    { new Guid("1c0dc3aa-f763-4c8b-83c0-32b5455655cb"), new Guid("07179785-65fd-4a8c-bc6d-81e0220518f9") },
                    { new Guid("1c0dc3aa-f763-4c8b-83c0-32b5455655cb"), new Guid("10c50b37-6044-4dcc-992d-03ca646425db") },
                    { new Guid("4f125acf-5f5f-4039-b515-cd35905a37de"), new Guid("90b029af-6d3d-47d6-9271-af0abb925d0e") },
                    { new Guid("4f125acf-5f5f-4039-b515-cd35905a37de"), new Guid("f185924c-ce14-45ae-820a-efcbaba77149") },
                    { new Guid("75d44fbc-d180-44f0-b07e-9226e6dceaf2"), new Guid("2b6d15a1-5f21-4fd7-a9c7-883e2ab83f4f") },
                    { new Guid("75d44fbc-d180-44f0-b07e-9226e6dceaf2"), new Guid("86d3dede-fdb8-4c18-af89-5976b7408c4c") },
                    { new Guid("7b2f5794-a072-4a44-ab22-5728ea03e41a"), new Guid("10c50b37-6044-4dcc-992d-03ca646425db") },
                    { new Guid("7b2f5794-a072-4a44-ab22-5728ea03e41a"), new Guid("2752d934-80d4-49a3-80af-58a5a05205ac") },
                    { new Guid("c8b2a272-30b4-4668-b75c-6da882be7323"), new Guid("08723ab2-f62a-4994-8c0d-9436c542dc0c") },
                    { new Guid("c8b2a272-30b4-4668-b75c-6da882be7323"), new Guid("90b029af-6d3d-47d6-9271-af0abb925d0e") },
                    { new Guid("e2e8a3bc-c44d-47ca-b088-35290b300760"), new Guid("08723ab2-f62a-4994-8c0d-9436c542dc0c") },
                    { new Guid("e2e8a3bc-c44d-47ca-b088-35290b300760"), new Guid("504018b3-9628-4d74-a8ff-8bbac60553a7") },
                    { new Guid("fe096551-1df3-43aa-b091-e3928a586594"), new Guid("07179785-65fd-4a8c-bc6d-81e0220518f9") },
                    { new Guid("fe096551-1df3-43aa-b091-e3928a586594"), new Guid("bad37d13-3906-49c4-b776-1e7e4b780955") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("002a5593-f9a5-4daf-92e8-1e6425306a06"), new Guid("2752d934-80d4-49a3-80af-58a5a05205ac") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("002a5593-f9a5-4daf-92e8-1e6425306a06"), new Guid("f185924c-ce14-45ae-820a-efcbaba77149") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("0b9ba76d-de9c-4095-b055-e1e5c39f809f"), new Guid("86d3dede-fdb8-4c18-af89-5976b7408c4c") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("0b9ba76d-de9c-4095-b055-e1e5c39f809f"), new Guid("bad37d13-3906-49c4-b776-1e7e4b780955") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("13240a79-cd16-4c5b-b974-9ee10084d17f"), new Guid("2b6d15a1-5f21-4fd7-a9c7-883e2ab83f4f") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("13240a79-cd16-4c5b-b974-9ee10084d17f"), new Guid("504018b3-9628-4d74-a8ff-8bbac60553a7") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("1c0dc3aa-f763-4c8b-83c0-32b5455655cb"), new Guid("07179785-65fd-4a8c-bc6d-81e0220518f9") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("1c0dc3aa-f763-4c8b-83c0-32b5455655cb"), new Guid("10c50b37-6044-4dcc-992d-03ca646425db") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("4f125acf-5f5f-4039-b515-cd35905a37de"), new Guid("90b029af-6d3d-47d6-9271-af0abb925d0e") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("4f125acf-5f5f-4039-b515-cd35905a37de"), new Guid("f185924c-ce14-45ae-820a-efcbaba77149") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("75d44fbc-d180-44f0-b07e-9226e6dceaf2"), new Guid("2b6d15a1-5f21-4fd7-a9c7-883e2ab83f4f") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("75d44fbc-d180-44f0-b07e-9226e6dceaf2"), new Guid("86d3dede-fdb8-4c18-af89-5976b7408c4c") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("7b2f5794-a072-4a44-ab22-5728ea03e41a"), new Guid("10c50b37-6044-4dcc-992d-03ca646425db") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("7b2f5794-a072-4a44-ab22-5728ea03e41a"), new Guid("2752d934-80d4-49a3-80af-58a5a05205ac") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("c8b2a272-30b4-4668-b75c-6da882be7323"), new Guid("08723ab2-f62a-4994-8c0d-9436c542dc0c") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("c8b2a272-30b4-4668-b75c-6da882be7323"), new Guid("90b029af-6d3d-47d6-9271-af0abb925d0e") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("e2e8a3bc-c44d-47ca-b088-35290b300760"), new Guid("08723ab2-f62a-4994-8c0d-9436c542dc0c") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("e2e8a3bc-c44d-47ca-b088-35290b300760"), new Guid("504018b3-9628-4d74-a8ff-8bbac60553a7") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("fe096551-1df3-43aa-b091-e3928a586594"), new Guid("07179785-65fd-4a8c-bc6d-81e0220518f9") });

            migrationBuilder.DeleteData(
                table: "fk_problem_categories",
                keyColumns: new[] { "categories_id", "problems_id" },
                keyValues: new object[] { new Guid("fe096551-1df3-43aa-b091-e3928a586594"), new Guid("bad37d13-3906-49c4-b776-1e7e4b780955") });

            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("79b9ff35-cce2-4eda-b076-bb91c4de2a7e"), new Guid("f66d83de-d0cd-4a42-9fa4-1bf581016fe9") });

            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("ca597483-af5c-4a11-8a12-c7332898280e"), new Guid("4c69ccd8-d987-488a-8605-6aad3a2163e9") });

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("002a5593-f9a5-4daf-92e8-1e6425306a06"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("0b9ba76d-de9c-4095-b055-e1e5c39f809f"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("13240a79-cd16-4c5b-b974-9ee10084d17f"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("1c0dc3aa-f763-4c8b-83c0-32b5455655cb"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("4f125acf-5f5f-4039-b515-cd35905a37de"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("75d44fbc-d180-44f0-b07e-9226e6dceaf2"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("7b2f5794-a072-4a44-ab22-5728ea03e41a"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("c8b2a272-30b4-4668-b75c-6da882be7323"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("e2e8a3bc-c44d-47ca-b088-35290b300760"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("fe096551-1df3-43aa-b091-e3928a586594"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("07179785-65fd-4a8c-bc6d-81e0220518f9"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("08723ab2-f62a-4994-8c0d-9436c542dc0c"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("10c50b37-6044-4dcc-992d-03ca646425db"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("2752d934-80d4-49a3-80af-58a5a05205ac"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("2b6d15a1-5f21-4fd7-a9c7-883e2ab83f4f"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("504018b3-9628-4d74-a8ff-8bbac60553a7"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("86d3dede-fdb8-4c18-af89-5976b7408c4c"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("90b029af-6d3d-47d6-9271-af0abb925d0e"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("bad37d13-3906-49c4-b776-1e7e4b780955"));

            migrationBuilder.DeleteData(
                table: "problems",
                keyColumn: "id",
                keyValue: new Guid("f185924c-ce14-45ae-820a-efcbaba77149"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("79b9ff35-cce2-4eda-b076-bb91c4de2a7e"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ca597483-af5c-4a11-8a12-c7332898280e"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("04629bec-7011-4f18-81b4-cd07b3717c06"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("13f24a9d-faae-4741-b2ab-feb26b7ea5b4"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("39718f61-7d56-42f3-ba22-7f9b0a1951d0"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("428b287b-6109-426b-a86e-0497077cf624"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("9d11abda-838c-40ba-bff5-5f7545abf156"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("a8b3664e-7e96-4076-84ea-eb616d298729"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("af12052c-8e8c-48fe-91ed-d38016d5ea79"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("c126319e-7de1-4905-9b46-e811c86253d9"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("e0235e56-3308-42c9-98e9-ce5fb5c71d95"));

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: new Guid("e727cd2c-58a2-4837-b96b-e7f397af7811"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("4c69ccd8-d987-488a-8605-6aad3a2163e9"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f66d83de-d0cd-4a42-9fa4-1bf581016fe9"));

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("0e781b8d-f8e4-4162-8cd8-7a9684333913"), "Administrator" },
                    { new Guid("bb36f988-a230-4e1f-b961-5949a1d72d47"), "User" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password_hash" },
                values: new object[,]
                {
                    { new Guid("9b3a70f6-2eaf-445d-ae45-3088561175cf"), "admin@example.com", "admin", "kphdQvOIz4z2k6X+qkpLEQ==:saqwy2esNT7Ivy72XLHv2hcvnP2hPSZn3IM40MdDIMg=" },
                    { new Guid("fde07caa-1a06-4a14-8de8-330fce2426eb"), "user@example.com", "user", "9ExStWwFOVJ/NHWss9ZHZQ==:4WZuF3NcrcccQiR7T6owhi3J+o9NjJWy43ECxGq0lxg=" }
                });

            migrationBuilder.InsertData(
                table: "fk_user_roles",
                columns: new[] { "roles_id", "users_id" },
                values: new object[,]
                {
                    { new Guid("0e781b8d-f8e4-4162-8cd8-7a9684333913"), new Guid("9b3a70f6-2eaf-445d-ae45-3088561175cf") },
                    { new Guid("bb36f988-a230-4e1f-b961-5949a1d72d47"), new Guid("fde07caa-1a06-4a14-8de8-330fce2426eb") }
                });
        }
    }
}

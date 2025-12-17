using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "problems",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "varchar(100)", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    description = table.Column<string>(type: "varchar(300)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_problems", x => x.id);
                    table.ForeignKey(
                        name: "fk_problems_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_problems_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    jwt_id = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    is_used = table.Column<bool>(type: "boolean", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    expired_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_refresh_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_image", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_images_users_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "varchar(300)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    problem_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_comments_problems_problem_id",
                        column: x => x.problem_id,
                        principalTable: "problems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "problem_image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    problem_id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_problem_image", x => x.id);
                    table.ForeignKey(
                        name: "fk_problem_images_id",
                        column: x => x.problem_id,
                        principalTable: "problems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    problem_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    points = table.Column<double>(type: "numeric(3,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ratings", x => x.id);
                    table.CheckConstraint("CK_Rating_Points_Range", "points >= 1.00 AND points <= 5.00");
                    table.ForeignKey(
                        name: "fk_ratings_problems_problem_id",
                        column: x => x.problem_id,
                        principalTable: "problems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ratings_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_comments_problem_id",
                table: "comments",
                column: "problem_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_user_id",
                table: "comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_fk_problem_categories_problems_id",
                table: "fk_problem_categories",
                column: "problems_id");

            migrationBuilder.CreateIndex(
                name: "ix_fk_user_roles_users_id",
                table: "fk_user_roles",
                column: "users_id");

            migrationBuilder.CreateIndex(
                name: "ix_problem_image_problem_id",
                table: "problem_image",
                column: "problem_id");

            migrationBuilder.CreateIndex(
                name: "ix_problems_status_id",
                table: "problems",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_problems_user_id",
                table: "problems",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_ratings_problem_id",
                table: "ratings",
                column: "problem_id");

            migrationBuilder.CreateIndex(
                name: "ix_ratings_user_id",
                table: "ratings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_image_user_id",
                table: "user_image",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "fk_problem_categories");

            migrationBuilder.DropTable(
                name: "fk_user_roles");

            migrationBuilder.DropTable(
                name: "problem_image");

            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "user_image");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "problems");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

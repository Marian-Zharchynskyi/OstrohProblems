using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndex_User_Problem_OnRating : Migration
    {
        /// <inheritdoc />
       protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_problems_users_user_id",
                table: "problems");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "problems",
                newName: "created_by_id");

            migrationBuilder.RenameIndex(
                name: "ix_problems_user_id",
                table: "problems",
                newName: "ix_problems_created_by_id");

            migrationBuilder.AddForeignKey(
                name: "fk_problems_users_created_by_id",
                table: "problems",
                column: "created_by_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "password_hash",
                value: "GnkYAp+/tVH8KN/DVyGecg==:xgkO/qCIPyKF77kcKY4Pm1U/mQEoUJmW/AJnoiSeQ0A=");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                column: "password_hash",
                value: "RAf6HjNH1wYKyix9nUMpbA==:edORBR0LRLKo33nXWbUd1ccEaHyut3kJsYctDS+oCR8=");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                column: "password_hash",
                value: "PKixVmMOM0AvjLJFgj6+SQ==:uvnUFLYpdhKWWwUeVsA6mzGI2nlyoeF7Ow4eQvTpxAM=");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), "Coordinator" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password_hash" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000011"), "user@ostroh.edu.ua", "Звичайний Користувач", "RAf6HjNH1wYKyix9nUMpbA==:edORBR0LRLKo33nXWbUd1ccEaHyut3kJsYctDS+oCR8=" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "coordinator@ostroh.edu.ua", "Координатор Острога", "PKixVmMOM0AvjLJFgj6+SQ==:uvnUFLYpdhKWWwUeVsA6mzGI2nlyoeF7Ow4eQvTpxAM=" }
                });

            migrationBuilder.InsertData(
                table: "fk_user_roles",
                columns: new[] { "roles_id", "users_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000012") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_ratings_user_id_problem_id",
                table: "ratings",
                columns: new[] { "user_id", "problem_id" },
                unique: true);
        }


        /// <inheritdoc />
       protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_ratings_user_id_problem_id",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "fk_problems_users_created_by_id",
                table: "problems");

            migrationBuilder.RenameIndex(
                name: "ix_problems_created_by_id",
                table: "problems",
                newName: "ix_problems_user_id");

            migrationBuilder.RenameColumn(
                name: "created_by_id",
                table: "problems",
                newName: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_problems_users_user_id",
                table: "problems",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000011") });

            migrationBuilder.DeleteData(
                table: "fk_user_roles",
                keyColumns: new[] { "roles_id", "users_id" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000012") });

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "password_hash",
                value: "jJyJrBXdCmVsSERzwUZBEw==:qa0Ha9J/i6kh3qjdLjskADdTPSfDWduIFGVj59sqzUw=");
        }
    }
}

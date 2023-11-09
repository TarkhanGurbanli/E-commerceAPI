using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FinalInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_AppUser_AppUserId",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_Roles_RoleId",
                table: "AppUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserRoles",
                table: "AppUserRoles");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "AppUserRoles",
                newName: "AppUsersRoles");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserRoles_RoleId",
                table: "AppUsersRoles",
                newName: "IX_AppUsersRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserRoles_AppUserId",
                table: "AppUsersRoles",
                newName: "IX_AppUsersRoles_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsersRoles",
                table: "AppUsersRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsersRoles_AppUser_AppUserId",
                table: "AppUsersRoles",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsersRoles_Roles_RoleId",
                table: "AppUsersRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsersRoles_AppUser_AppUserId",
                table: "AppUsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsersRoles_Roles_RoleId",
                table: "AppUsersRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsersRoles",
                table: "AppUsersRoles");

            migrationBuilder.RenameTable(
                name: "AppUsersRoles",
                newName: "AppUserRoles");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsersRoles_RoleId",
                table: "AppUserRoles",
                newName: "IX_AppUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsersRoles_AppUserId",
                table: "AppUserRoles",
                newName: "IX_AppUserRoles_AppUserId");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserRoles",
                table: "AppUserRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_AppUser_AppUserId",
                table: "AppUserRoles",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_Roles_RoleId",
                table: "AppUserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

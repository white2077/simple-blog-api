using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreRestfulApi.Migrations
{
    /// <inheritdoc />
    public partial class user_can_crate_post : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Post_user_id",
                table: "Post",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_user_id",
                table: "Post",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_user_id",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_user_id",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Post");
        }
    }
}

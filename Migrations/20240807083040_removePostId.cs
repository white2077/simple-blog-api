using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreRestfulApi.Migrations
{
    /// <inheritdoc />
    public partial class removePostId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_Post_PostId",
                table: "comment");

            migrationBuilder.DropIndex(
                name: "IX_comment_PostId",
                table: "comment");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "comment");

            migrationBuilder.CreateIndex(
                name: "IX_comment_post_id",
                table: "comment",
                column: "post_id");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_Post_post_id",
                table: "comment",
                column: "post_id",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_Post_post_id",
                table: "comment");

            migrationBuilder.DropIndex(
                name: "IX_comment_post_id",
                table: "comment");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_comment_PostId",
                table: "comment",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_Post_PostId",
                table: "comment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

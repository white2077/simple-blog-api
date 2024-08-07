using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreRestfulApi.Migrations
{
    /// <inheritdoc />
    public partial class add_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_comment_comment_id",
                table: "comment");

            migrationBuilder.RenameColumn(
                name: "comment_id",
                table: "comment",
                newName: "ParentCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_comment_comment_id",
                table: "comment",
                newName: "IX_comment_ParentCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_comment_ParentCommentId",
                table: "comment",
                column: "ParentCommentId",
                principalTable: "comment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_comment_ParentCommentId",
                table: "comment");

            migrationBuilder.RenameColumn(
                name: "ParentCommentId",
                table: "comment",
                newName: "comment_id");

            migrationBuilder.RenameIndex(
                name: "IX_comment_ParentCommentId",
                table: "comment",
                newName: "IX_comment_comment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_comment_comment_id",
                table: "comment",
                column: "comment_id",
                principalTable: "comment",
                principalColumn: "Id");
        }
    }
}

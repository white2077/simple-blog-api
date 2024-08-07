using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreRestfulApi.Migrations
{
    /// <inheritdoc />
    public partial class add_fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_comment_ParentCommentId",
                table: "comment");

            migrationBuilder.DropIndex(
                name: "IX_comment_ParentCommentId",
                table: "comment");

            migrationBuilder.AddColumn<int>(
                name: "comment_id",
                table: "comment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_comment_comment_id",
                table: "comment",
                column: "comment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_comment_comment_id",
                table: "comment",
                column: "comment_id",
                principalTable: "comment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_comment_comment_id",
                table: "comment");

            migrationBuilder.DropIndex(
                name: "IX_comment_comment_id",
                table: "comment");

            migrationBuilder.DropColumn(
                name: "comment_id",
                table: "comment");

            migrationBuilder.CreateIndex(
                name: "IX_comment_ParentCommentId",
                table: "comment",
                column: "ParentCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_comment_ParentCommentId",
                table: "comment",
                column: "ParentCommentId",
                principalTable: "comment",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreRestfulApi.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullParentCmt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_comment_comment_id",
                table: "comment");

            migrationBuilder.AlterColumn<int>(
                name: "comment_id",
                table: "comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "comment_id",
                table: "comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_comment_comment_comment_id",
                table: "comment",
                column: "comment_id",
                principalTable: "comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

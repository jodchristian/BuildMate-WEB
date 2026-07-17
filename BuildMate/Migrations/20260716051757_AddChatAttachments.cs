using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMate.Migrations
{
    /// <inheritdoc />
    public partial class AddChatAttachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttachmentFileName",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentPath",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsImage",
                table: "ChatMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentFileName",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "AttachmentPath",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "IsImage",
                table: "ChatMessages");
        }
    }
}

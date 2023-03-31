using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackoveflowClone.Migrations
{
    /// <inheritdoc />
    public partial class MinusGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Users",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "Gender");
        }
    }
}

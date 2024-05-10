using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sakany.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class et : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Governorate",
                table: "Properties",
                newName: "GovernorateID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GovernorateID",
                table: "Properties",
                newName: "Governorate");
        }
    }
}

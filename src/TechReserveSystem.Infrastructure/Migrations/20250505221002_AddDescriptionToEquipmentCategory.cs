using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechReserveSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToEquipmentCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EquipmentCategories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "EquipmentCategories");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechReserveSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserReservationsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE VIEW UserReservations AS
        SELECT 
            r.Id AS ReservationId,
            r.UserId,
            u.Name AS UserName,
            r.EquipmentId,
            e.Name AS EquipmentName,
            r.Quantity,
            r.StartDate,
            r.ExpectedReturnDate,
            r.Status
        FROM EquipmentReservations r
        JOIN Users u ON r.UserId = u.Id
        JOIN Equipments e ON r.EquipmentId = e.Id;
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW UserReservations;");
        }
    }
}

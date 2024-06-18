using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspMedSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserTreatmentsNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "UserTreatments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "UserTreatments");
        }
    }
}

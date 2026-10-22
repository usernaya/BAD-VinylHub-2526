using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinylHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBorrowerNameToLoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BorrowerName",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowerName",
                table: "Loans");
        }
    }
}

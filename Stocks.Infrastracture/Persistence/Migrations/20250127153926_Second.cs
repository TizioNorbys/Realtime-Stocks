using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stocks.Infrastracture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Stocks",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}

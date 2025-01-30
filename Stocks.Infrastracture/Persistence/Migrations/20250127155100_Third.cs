using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stocks.Infrastracture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Stocks",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Stocks",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Stocks",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepositService.Migrations
{
    public partial class CleanUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blocktime",
                table: "DepositHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Blocktime",
                table: "DepositHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }
    }
}

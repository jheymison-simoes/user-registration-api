using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserRegister.Data.Migrations
{
    public partial class FixRelacionShipUserAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_address_id",
                table: "address");

            migrationBuilder.CreateIndex(
                name: "ix_user_address_id",
                table: "user",
                column: "address_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_user_address_id",
                table: "user",
                column: "address_id",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_address_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_user_address_id",
                table: "user");

            migrationBuilder.AddForeignKey(
                name: "fk_user_address_id",
                table: "address",
                column: "id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

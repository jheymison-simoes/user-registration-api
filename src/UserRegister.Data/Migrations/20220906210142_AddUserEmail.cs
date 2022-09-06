using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserRegister.Data.Migrations
{
    public partial class AddUserEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "address_id",
                table: "user",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 8);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "user");

            migrationBuilder.AlterColumn<Guid>(
                name: "address_id",
                table: "user",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerAccount.DAL.Migrations
{
    public partial class addNumOfAttempsCodeNumcolumnstoEmailVerificationtableforretrylimitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VerificationCode",
                table: "EmailVerifications",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AddColumn<int>(
                name: "CodeNum",
                table: "EmailVerifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumOfAttemps",
                table: "EmailVerifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeNum",
                table: "EmailVerifications");

            migrationBuilder.DropColumn(
                name: "NumOfAttemps",
                table: "EmailVerifications");

            migrationBuilder.AlterColumn<string>(
                name: "VerificationCode",
                table: "EmailVerifications",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);
        }
    }
}

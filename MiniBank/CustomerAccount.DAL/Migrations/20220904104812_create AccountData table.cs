using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerAccount.DAL.Migrations
{
    public partial class createAccountDatatable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountDatas_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDatas_CustomerId",
                table: "AccountDatas",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountDatas");
        }
    }
}

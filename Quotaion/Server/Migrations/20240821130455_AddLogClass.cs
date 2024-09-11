using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotaion.Server.Migrations
{
    public partial class AddLogClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAction = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogClasses_Users_UserAction",
                        column: x => x.UserAction,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogClasses_UserAction",
                table: "LogClasses",
                column: "UserAction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogClasses");
        }
    }
}

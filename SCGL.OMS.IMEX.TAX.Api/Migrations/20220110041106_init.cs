using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCGL.OMS.IMEX.TAX.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DOCUMENTS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    NO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCUMENTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GROUP_DOCUMENTS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    NO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GROUP_DOCUMENTS", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOCUMENTS");

            migrationBuilder.DropTable(
                name: "GROUP_DOCUMENTS");
        }
    }
}

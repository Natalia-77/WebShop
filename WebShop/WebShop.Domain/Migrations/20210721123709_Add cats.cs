using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebShop.Domain.Migrations
{
    public partial class Addcats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Image = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DateCreate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateDelete = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCats");
        }
    }
}

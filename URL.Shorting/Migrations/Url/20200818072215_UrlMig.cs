﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace URL.Shorting.Migrations.Url
{
    public partial class UrlMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ShortUrl = table.Column<string>(nullable: true),
                    NumOfClick = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlTable", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlTable");
        }
    }
}

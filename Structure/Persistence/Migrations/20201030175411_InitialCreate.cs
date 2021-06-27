using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exceptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationName = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    IsProtected = table.Column<bool>(nullable: false),
                    Host = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    HTTPMethod = table.Column<string>(nullable: true),
                    IPAddress = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    StatusCode = table.Column<string>(nullable: true),
                    DeletionDate = table.Column<DateTime>(nullable: false),
                    LastLogDate = table.Column<DateTime>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    TraceId = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: true),
                    RequestBody = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exceptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exceptions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

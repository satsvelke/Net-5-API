using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddExceptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exceptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationName = table.Column<string>(nullable: true),
                    MachineName = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    IsProtected = table.Column<bool>(nullable: false),
                    Host = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    HTTPMethod = table.Column<string>(nullable: true),
                    IPAddress = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    StatusCode = table.Column<string>(nullable: true),
                    DeletionDate = table.Column<DateTime>(nullable: false),
                    FullJson = table.Column<string>(nullable: true),
                    ErrorHash = table.Column<string>(nullable: true),
                    DuplicateCount = table.Column<int>(nullable: false),
                    LastLogDate = table.Column<DateTime>(nullable: false),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exceptions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exceptions");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prescriptions.Migartions
{
    public partial class AddUserAndRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "User",
                table => new
                {
                    IdUser = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>("nvarchar(max)", nullable: true),
                    Password = table.Column<string>("nvarchar(max)", nullable: true),
                    Salt = table.Column<string>("nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>("nvarchar(max)", nullable: true),
                    RefreshTokenExp = table.Column<DateTime>("datetime2", nullable: true)
                },
                constraints: table => { table.PrimaryKey("User_pk", x => x.IdUser); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "User");
        }
    }
}
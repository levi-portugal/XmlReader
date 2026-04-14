using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XmlReader.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilesTable",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesTable", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Xmls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    XmlNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuerDocument = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    SocialReasonIssuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialReasonRecipient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ServiceTakerCnpj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipperCnpj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xmls", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesTable");

            migrationBuilder.DropTable(
                name: "Xmls");
        }
    }
}

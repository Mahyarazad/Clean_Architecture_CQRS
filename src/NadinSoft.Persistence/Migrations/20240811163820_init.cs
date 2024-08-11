using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NadinSoft.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProductDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManufactureEmail = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ManufacturePhone = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "UniqueIndex_MEmail_PDate",
                table: "Products",
                columns: new[] { "ManufactureEmail", "ProductDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

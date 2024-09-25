using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkSample.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 25, 0, 10, 41, 126, DateTimeKind.Local).AddTicks(5320), new DateTime(2024, 9, 25, 0, 10, 41, 126, DateTimeKind.Local).AddTicks(5404), "Max", "Mustermann" },
                    { 2, new DateTime(2024, 9, 25, 0, 10, 41, 126, DateTimeKind.Local).AddTicks(5440), new DateTime(2024, 9, 25, 0, 10, 41, 126, DateTimeKind.Local).AddTicks(5474), "Erika", "Musterfrau" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borealis2tsx.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataLine",
                columns: table => new
                {
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temperature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pressure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Altitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccZ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GyroX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GyroY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GyroZ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MagX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MagY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MagZ = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataLine");
        }
    }
}

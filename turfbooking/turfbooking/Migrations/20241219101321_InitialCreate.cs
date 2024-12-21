using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace turfbooking.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlotBookings",
                columns: table => new
                {
                    bookingid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    slotid = table.Column<int>(type: "int", nullable: false),
                    custname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotBookings", x => x.bookingid);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    slotid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    slottime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    slotdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.slotid);
                });

            migrationBuilder.CreateTable(
                name: "TurfUsers",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usertype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurfUsers", x => x.userid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlotBookings");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "TurfUsers");
        }
    }
}
